# sqlC - OperativeDB

מערכת ניהול סוכנים ומבצעים עבור בסיס נתונים MySQL.

## תיאור

הפרויקט מספק ממשק לניהול סוכנים, כולל הוספה, עדכון, מחיקה, חיפוש וספירה לפי סטטוס. הקוד ממומש ב-#C ומתחבר למסד נתונים MySQL.

## תכונות עיקריות

- הוספה, עדכון ומחיקת סוכנים
- חיפוש סוכנים לפי קוד
- ספירת סוכנים לפי סטטוס
- שימוש ב-Builder Pattern ליצירת אובייקט Agent
- הדפסת רשימות סוכנים וסטטיסטיקות

## מבנה הפרויקט

- `models/Agent.cs` - מחלקת Agent ומחלקת Builder ליצירת סוכנים
- `AgentDAL.cs` - גישה לנתונים וביצוע פעולות CRUD על סוכנים
- `MySqlData.cs` - ניהול חיבור למסד נתונים MySQL
- `Program.cs` - נקודת הכניסה הראשית של התוכנית

## דרישות

- .NET (גרסה עדכנית)
- MySQL Database

## התקנה והרצה

1. שיבץ את פרטי החיבור למסד הנתונים בקובץ `MySqlData.cs`.
2. הרץ את הפרויקט באמצעות Visual Studio Code או פקודת dotnet:
   ```sh
   dotnet run
   ```

## דוגמת שימוש

```csharp
Agent agent = new Agent.Builder()
    .SetCodeName("4")
    .SetLocation("isrel")
    .SetRealName("mose")
    .SetStatus("Active")
    .SetMissionsCompleted(5)
    .Build();

agentDAL.Add(agent);
```

## תרומה

נשמח לקבל Pull Requests עם שיפורים או תיקונים.

## רישיון

MIT License
