from models import db, Attendance
from datetime import datetime
import pytz
from flask import request

class AttendanceSystem:
    def __init__(self):
        self.CUTOFF_HOUR = 9
        self.israel_tz = pytz.timezone('Asia/Jerusalem')
        self.SOLDIERS = [
            "מנחם מענדל אייזנבך",
            "משה כהן",
            "חגי טובין",
            "אור זלינגר",
            "אוראל דוידוב",
            "יצחק דמן",
            "משה גבריאל טאלר",
            "דן חמד",
            "יהונתן בן עמי",
            "אליהו גלעד יומטוביאן",
            "אשר וסרמן",
            "ממשרש יעקוב",
            "סטון אלחנן מאיר",
            "פישר דניאל גבע",
            "פישר אלי",
            "פרנקרייך יצחק",
            "צירקוס שניאור זלמן",
            "קופרמן מנחם מנדל",
            "קליר נתנאל",
            "רבינוביץ אלעזר",
            "ריזה ישראל",
            "שבדרון חיים",
            "שובר אריה"
        ]

    def after_cutoff(self):
        now = datetime.now(self.israel_tz)
        return now.hour >= self.CUTOFF_HOUR

    def get_real_ip(self, request):
        x_forwarded_for = request.headers.get('X-Forwarded-For')
        if x_forwarded_for:
            return x_forwarded_for.split(',')[0].strip()
        return request.remote_addr

    def update_attendance(self, name, status, ip_address, user_agent):
        if self.after_cutoff():
            return {'error': 'עבר הזמן!'}, 403

        if not name or not status:
            return {'error': 'שדות חסרים'}, 400

        if name not in self.SOLDIERS:
            return {'error': 'שם לא חוקי'}, 400

        now = datetime.now(self.israel_tz)
        today = now.strftime('%Y-%m-%d')
        device_type = self.detect_device_type(user_agent)

        record = Attendance.query.filter_by(date=today, name=name).first()

        if record:
            record.status = status
            record.timestamp = now
            record.ip_address = ip_address
            record.user_agent = user_agent
            record.device_type = device_type
        else:
            new_record = Attendance(
                date=today,
                name=name,
                status=status,
                ip_address=ip_address,
                user_agent=user_agent,
                device_type=device_type,
                timestamp=now  # שים לב!
            )
            db.session.add(new_record)

        db.session.commit()
        return {'success': True}, 200

    def detect_device_type(self, user_agent):
        if not user_agent:
            return 'Unknown'

        user_agent = user_agent.lower()

        if 'mobile' in user_agent:
            return 'Mobile'
        elif 'tablet' in user_agent:
            return 'Tablet'
        elif 'android' in user_agent:
            return 'Mobile (Android)'
        elif 'iphone' in user_agent:
            return 'Mobile (iPhone)'
        elif 'ipad' in user_agent:
            return 'Tablet (iPad)'
        elif 'windows' in user_agent:
            return 'Desktop (Windows)'
        elif 'macintosh' in user_agent:
            return 'Desktop (Mac)'
        elif 'linux' in user_agent:
            return 'Desktop (Linux)'
        else:
            return 'Desktop (Other)'

    def get_attendance(self):
        today = datetime.now().strftime('%Y-%m-%d')
        records = Attendance.query.filter_by(date=today).all()

        attendance = {soldier: 'לא נוכח' for soldier in self.SOLDIERS}
        for record in records:
            attendance[record.name] = record.status

        return attendance
    
    def get_soldiers(self):
        today = datetime.now().strftime('%Y-%m-%d')
        records = Attendance.query.filter_by(date=today).all()
        soldiers = self.SOLDIERS.copy();

        for record in records:
            soldiers.remove(record.name)
        return soldiers

    def get_attendance_details(self):
        today = datetime.now().strftime('%Y-%m-%d')
        records = Attendance.query.filter_by(date=today).all()
        records_dict = {record.name: record for record in records}

        details = []
        for soldier in self.SOLDIERS:
            if soldier in records_dict:
                record = records_dict[soldier]
                details.append({
                    'name': soldier,
                    'status': record.status,
                    'timestamp': record.timestamp.isoformat() if record.timestamp else None,
                    'ip_address': record.ip_address,
                    'device_type': record.device_type,
                    'reported': True
                })
            else:
                details.append({
                    'name': soldier,
                    'status': 'לא נוכח',
                    'timestamp': None,
                    'ip_address': None,
                    'device_type': None,
                    'reported': False
                })
        return details