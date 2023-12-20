namespace DO;


/// <summary>
/// Engineer class with all the information about him
/// <param name="Id">The id of the engineer</param>
/// <param name="Name">the name of the engineer</param>
/// <param name="Email">the email address of the engineer</param>
/// <param name="Level">the experience level of the engineer</param>
/// </summary>
public record Engineer
{
    public int Id { set; get; }
    public string Name { set; get; }  = "";
    public string Email { set; get; } = "";
    public EngineerExperience? Level { set; get; }
    public Engineer(int myId, string myName, string myEmail,EngineerExperience mylevel)
    {
        Id = myId;
        Name = myName;  
        Email = myEmail;
        Level = mylevel;
    }
    public Engineer():this(0,"","", EngineerExperience.Expert) { }
    public override string ToString()
    {
        return $"Engineer ID: {Id}\n" +
               $"Name: {Name}\n" +
               $"Email: {Email}\n" +
               $"Level: {Level}\n";
    }

}
