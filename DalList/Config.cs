namespace DalList;

public class Config
{
    private static int nextId = 1; // מזהה רץ

    public int Id { get; } // פרופרטי מזהה

     

    public Config()
    {
        Id = nextId; // הכנסת מזהה לאובייקט
        nextId++; // תיעוד מזהה רץ לשימוש באובייקט הבא
    }
}