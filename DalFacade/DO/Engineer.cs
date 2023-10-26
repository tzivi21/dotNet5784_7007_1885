namespace DO;

enum EngineerExperience
{
    Enginner
}
/// <summary>
/// Engineer class with all the information about him
/// <param name="Id">The id of the engineer</param>
/// <param name="Name">the name of the engineer</param>
/// <param name="Email">the email address of the engineer</param>
/// <param name="Level">the experience level of the engineer</param>
/// </summary>
public record Engineer
{
    
    int Id { set; get; }
    string Name { set; get; } = null;
    string Email { set; get; } = null;
    EngineerExperience Level { set; get; } = EngineerExperience.Enginner;
    public Engineer(int myId, string myName, string myEmail)
    {
        Id = myId;
        Name = myName;  
        Email = myEmail;
    }
    public Engineer() { }
}
