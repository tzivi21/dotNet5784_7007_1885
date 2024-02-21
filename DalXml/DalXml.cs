namespace Dal;
using DalApi;
using System.Xml;

//stage 3
sealed internal class DalXml : IDal
{
    public static DalXml Instance {  get; } = new DalXml();
    private DalXml() { }
    public IDependency Dependency => new DependencyImplementation();

    public ITask Task =>  new TaskImplementation();

    public IEngineer Engineer =>  new EngineerImplementation();
    public void Reset()
    {
        XMLTools.ResetFile("Engineers", "ArrayOfEngineer");
        XMLTools.ResetFile("Tasks", "ArrayOfTask");
        XMLTools.ResetFile("depndencies", "ArrayOfDependencies");
    }
    public DateTime? EndProjectDate
    {
        get => XMLTools.GetDates("data-config", "EndProjectDate");

    }
    public DateTime? StartProjectDate
    {
        get => XMLTools.GetDates("data-config", "StartProjectDate");
    }
    public void ResetConfiguration()
    {
        // Load the XML file
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load("config.xml");

        // Get the elements to update
        XmlNode nextTaskIdNode = xmlDoc.SelectSingleNode("/config/NextTaskId");
        XmlNode nextDependencyIdNode = xmlDoc.SelectSingleNode("/config/NextDependencyId");

        // Update the values
        nextTaskIdNode.InnerText = "100"; // Set NextTaskId to 100
        nextDependencyIdNode.InnerText = "250"; // Set NextDependencyId to 250
        // Save the modified XML back to the file
        xmlDoc.Save("config.xml");

        Console.WriteLine("Values updated successfully.");
    }
}
