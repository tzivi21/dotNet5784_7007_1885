using Dal; 
using DalApi;
using DO;
using DalTest;
using System.Reflection;

class Program
{
    private static ITask? s_dalTask = new TaskImplementation();
    private static IEngineer? s_dalEngineer = new EngineerImplementation();
    private static IDependency? s_dalDependency = new DependencyImplementation();

    static void Main()
    {
        try {
            Initialization.Do(s_dalDependency, s_dalTask,s_dalEngineer);
            mainMenu();
        }
        catch ( Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        
    }

    static void mainMenu()
    {
        while (true)
        {
            Console.WriteLine("תפריט ראשי - בחר ישות שברצונך לבדוק:");
            Console.WriteLine("0. יציאה מתפריט ראשי");
            Console.WriteLine("1. משימה");
            Console.WriteLine("2. תלות");
            Console.WriteLine("3. מהנדס");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 0:
                        Console.WriteLine("יצאת מהתוכנית.");
                        return;
                    case 1:
                        taskMenu();
                        break;
                    case 2:
                        dependencyMenu();
                        break;
                    case 3:
                        engineerMenu();
                        break;
                    default:
                        Console.WriteLine("אפשרות לא חוקית. אנא בחר מהתפריט.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("אנא הכנס מספר חוקי.");
            }
        }
    }//Manages the main menu
    static void taskMenu()
    {
        while (true)
        {
            Console.WriteLine("בחר את המתודה שברצונך לבצע:");
            Console.WriteLine("1. יציאה מתפריט ראשי");
            Console.WriteLine("2. הוספת אובייקט חדש (Create)");
            Console.WriteLine("3. תצוגת אובייקט על פי מזהה (Read)");
            Console.WriteLine("4. תצוגת רשימת כל האובייקטים (ReadAll)");
            Console.WriteLine("5. עדכון נתוני אובייקט קיים (Update)");
            Console.WriteLine("6. מחיקת אובייקט קיים (Delete)");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                try { 
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("יצאת מהתוכנית.");
                            return;
                        case 2:
                            createTask();
                            break;
                        case 3:
                            readTask();
                            break;
                        case 4:
                            readAllTasks();
                            break;
                        case 5:
                            updateTask();
                            break;
                        case 6:
                            deleteTask();
                            break;
                    
                        default:
                            Console.WriteLine("אפשרות לא חוקית. אנא בחר מהתפריט.");
                            break;
                    }
                }
                catch (Exception e) { Console.WriteLine(e.ToString()); }
            }
            else
            {
                Console.WriteLine("אנא הכנס מספר חוקי.");
            }
        }
    }//Manages the task menu
    static void engineerMenu()
    {
        while (true)
        {
            Console.WriteLine("בחר את המתודה שברצונך לבצע:");
            Console.WriteLine("1. יציאה מתפריט ראשי");
            Console.WriteLine("2. הוספת אובייקט חדש (Create)");
            Console.WriteLine("3. תצוגת אובייקט על פי מזהה (Read)");
            Console.WriteLine("4. תצוגת רשימת כל האובייקטים (ReadAll)");
            Console.WriteLine("5. עדכון נתוני אובייקט קיים (Update)");
            Console.WriteLine("6. מחיקת אובייקט קיים (Delete)");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                try
                {
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("יצאת מהתוכנית.");
                            return;
                        case 2:
                            createEngineer();
                            break;
                        case 3:
                            readEngineer();
                            break;
                        case 4:
                            readAllEngineers();
                            break;
                        case 5:
                            updateEngineer();
                            break;
                        case 6:
                            deleteEngineer();
                            break;

                        default:
                            Console.WriteLine("אפשרות לא חוקית. אנא בחר מהתפריט.");
                            break;
                    }
                }
                catch (Exception e) { Console.WriteLine(e.ToString()); };
            }
            else
            {
                Console.WriteLine("אנא הכנס מספר חוקי.");
            }
        }
    }//Manages the engineer menu
    static void dependencyMenu()
    {
        while (true)
        {
            Console.WriteLine("בחר את המתודה שברצונך לבצע:");
            Console.WriteLine("1. יציאה מתפריט ראשי");
            Console.WriteLine("2. הוספת אובייקט חדש (Create)");
            Console.WriteLine("3. תצוגת אובייקט על פי מזהה (Read)");
            Console.WriteLine("4. תצוגת רשימת כל האובייקטים (ReadAll)");
            Console.WriteLine("5. עדכון נתוני אובייקט קיים (Update)");
            Console.WriteLine("6. מחיקת אובייקט קיים (Delete)");

            int choice;
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                try
                {
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("יצאת מהתוכנית.");
                            return;
                        case 2:
                            createDependency();
                            break;
                        case 3:
                            readDependency();
                            break;
                        case 4:
                            readAllDependencies();
                            break;
                        case 5:
                            updateDependency();
                            break;
                        case 6:
                            deleteDependency();
                            break;

                        default:
                            Console.WriteLine("אפשרות לא חוקית. אנא בחר מהתפריט.");
                            break;
                    }
                }
                catch(Exception e) { Console.WriteLine(e.ToString()); }
            }
            else
            {
                Console.WriteLine("אנא הכנס מספר חוקי.");
            }
        }
    }//Manages the dependency menu
    static void createTask() {
        int id;
        DO.Task new_task = ReadTaskFromUser();
        if (new_task != null)
        {
            id = s_dalTask.Create(new_task);
            Console.WriteLine($"המשימה נוצרה בהצלחה, המספר המזהה הוא: {id}");
        }
    }//create a new task
    static void readTask()
    {
        int id;
        Console.WriteLine("הכנס מספר מזהה של המשימה שתרצה לראות:");
        int.TryParse(Console.ReadLine(), out id);

        DO.Task? task = s_dalTask.Read(id);
        if(task != null)
        {
            Console.WriteLine(task);
        }
        else
        {
            throw new Exception($"אובייקט מסוג Task עם ID {id} לא קיים");
        }

    }//prints the task by it's id
    static void readAllTasks()
    {
        List<DO.Task> tasks = s_dalTask.ReadAll();
        if (tasks == null)
        {
            throw new Exception("הרשימה אינה קיימת");
        }
        foreach (DO.Task task in tasks)
        {
            Console.WriteLine(task);
        }
    }//prits the all list of tasks
    static void updateTask()
    {
        int id;
        Console.WriteLine("הכנס מספר מזהה של המשימה שברצונך לעדכן:");
        int.TryParse(Console.ReadLine(), out id);
        DO.Task? task = s_dalTask.Read(id);
        if(task != null) {
            Console.WriteLine(task);
            DO.Task updatedTask= ReadTaskFromUser();
            FieldInfo[] fields = updatedTask.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            // Loop through each field
            foreach (FieldInfo field in fields)
            {
                
                object? fieldValue = field.GetValue(updatedTask);
                if (fieldValue == null|| DateTime.Equals(fieldValue, DateTime.MinValue)|| fieldValue == "")
                {
                    field.SetValue(updatedTask, field.GetValue(task));
                }
            }
            updatedTask.Id = id;//update the new task's id to be the same as the task the user want to update
            s_dalTask.Update(updatedTask);
            Console.WriteLine(s_dalTask.Read(id));//prints the task after the update
        }

    }//update a specific task by the user's input
    static void deleteTask()
    {
        int id;
        Console.WriteLine("הכנס מספר מזהה של המשימה שברצונך למחוק:");
        int.TryParse(Console.ReadLine(), out id);
        s_dalTask.Delete(id);
        Console.WriteLine("האובייקט נמחק בהצלחה");
    }//delete a task by it's id from the user's input
    static DO.Task ReadTaskFromUser()
    {
        Console.WriteLine("Enter Task Data:");
        // Input fields
        Console.Write("Description: ");
        string? description = Console.ReadLine();

        Console.Write("Alias: ");
        string? alias = Console.ReadLine();

        Console.Write("Milestone (true/false): ");
        bool milestone1;
        bool? milestone=null;
        if (bool.TryParse(Console.ReadLine(), out milestone1))
        {
            milestone = milestone1;
        }

        Console.Write("Start Date (yyyy-MM-dd HH:mm:ss): ");
        DateTime start= DateTime.MinValue;
        DateTime.TryParse(Console.ReadLine(), out start);
 
        Console.Write("Schedule Date (yyyy-MM-dd HH:mm:ss): ");
        DateTime scheduleDate= DateTime.MinValue;
        DateTime.TryParse(Console.ReadLine(), out scheduleDate);


        Console.Write("Deadline (yyyy-MM-dd HH:mm:ss): ");
        DateTime deadline= DateTime.MinValue;
        DateTime.TryParse(Console.ReadLine(), out deadline);

        Console.Write("Complete Date (yyyy-MM-dd HH:mm:ss): ");
        DateTime complete = DateTime.MinValue;

        DateTime.TryParse(Console.ReadLine(), out complete);

        Console.Write("Deliverables: ");
        string? deliverables = Console.ReadLine();

        Console.Write("Remarks: ");
        string? remarks = Console.ReadLine();

        Console.Write("Engineer ID: ");
        int engineerId1;
        int? engineerId=null;

        if (int.TryParse(Console.ReadLine(), out engineerId1))
        {
            engineerId= engineerId1;
        }

        Console.WriteLine("Choose Engineer Experience Level:");
        Console.WriteLine("1. Expert");
        Console.WriteLine("2. Novice");
        Console.WriteLine("3. AdvancedBeginner");
        Console.WriteLine("4. Competent");
        Console.WriteLine("5. Proficient");

        Console.Write("Enter the corresponding number: ");

        EngineerExperience? complexityLevel;
        if (int.TryParse(Console.ReadLine(), out int levelChoice))
        {
            

            switch (levelChoice)
            {
                case 1:
                    complexityLevel = EngineerExperience.Expert;
                    break;
                case 2:
                    complexityLevel = EngineerExperience.Novice;
                    break;
                case 3:
                    complexityLevel = EngineerExperience.AdvancedBeginner;
                    break;
                case 4:
                    complexityLevel = EngineerExperience.Competent;
                    break;
                case 5:
                    complexityLevel = EngineerExperience.Proficient;
                    break;
                default:
                    complexityLevel = null;
                    break;
            }
        }
        else
        {
            complexityLevel = null;
        }


        // Create and return a new Task object
        return  new DO.Task
        {
            Description = description,
            Alias = alias,
            Milestone = milestone,
            Start = start,
            ScheduleDate = scheduleDate,
            DeadLine = deadline,
            Complete = complete,
            Deliverables = deliverables,
            Remarks = remarks,
            Engineerid = engineerId,
            ComplexityLevel = complexityLevel
        };
    }//reads the task values from the user

    static void createEngineer()
    {
        DO.Engineer newEngineer = ReadEngineerFromUser();
        if (newEngineer != null)
            s_dalEngineer.Create(newEngineer);
    }//create a new engineer

    static void readEngineer()
    {
        int id;
        Console.WriteLine("הכנס מספר מזהה של המהנדס שתרצה לראות:");
        int.TryParse(Console.ReadLine(), out id);

        DO.Engineer? engineer = s_dalEngineer.Read(id);
        if (engineer != null)
        {
            Console.WriteLine(engineer);
        }
        else
        {
            throw new Exception($"אובייקט מסוג Engineer עם ID {id} לא קיים");
        }
    }//prints the engineer by it's id

    static void readAllEngineers()
    {
        List<DO.Engineer> engineers = s_dalEngineer.ReadAll();
        if (engineers == null)
        {
            throw new Exception("הרשימה אינה קיימת");
        }
        foreach (DO.Engineer engineer in engineers)
        {
            Console.WriteLine(engineer);
        }
    }//prits the all list of engineers

    static void updateEngineer()
    {
        int id;
        Console.WriteLine("הכנס מספר מזהה של המהנדס שברצונך לעדכן:");
        int.TryParse(Console.ReadLine(), out id);
        DO.Engineer? engineer = s_dalEngineer.Read(id);
        if (engineer != null)
        {
            Console.WriteLine(engineer);
            DO.Engineer updatedEngineer = ReadEngineerFromUser();
            FieldInfo[] fields = updatedEngineer.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            // Loop through each field
            foreach (FieldInfo field in fields)
            {
                //string fieldName = field.Name;
                object? fieldValue = field.GetValue(updatedEngineer);
                if (fieldValue == null|| fieldValue=="")
                {
                    field.SetValue(updatedEngineer, field.GetValue(engineer));
                }
            }
            s_dalEngineer.Update(updatedEngineer);
            Console.WriteLine(s_dalEngineer.Read(id));//prints the updated engineer
        }
    }//update a specific engineer by the user's input

    static void deleteEngineer()
    {
        int id;
        Console.WriteLine("הכנס מספר מזהה של המהנדס שברצונך למחוק:");
        int.TryParse(Console.ReadLine(), out id);
        s_dalEngineer.Delete(id);
        Console.WriteLine("האובייקט נמחק בהצלחה");
    }//delete a engineer by it's id from the user's input
    static void createDependency()
    {
        int id;
        DO.Dependency newDependency = ReadDependencyFromUser();
        if (newDependency != null)
        {
            id=s_dalDependency.Create(newDependency);
            Console.WriteLine($"התלות נוצרה בהצלחה, המספר המזהה הוא:{id}");

        }
    }//create a new dependency

    static void readDependency()
    {
        int id;
        Console.WriteLine("הכנס מספר מזהה של התלות שתרצה לראות:");
        int.TryParse(Console.ReadLine(), out id);

        DO.Dependency? dependency = s_dalDependency.Read(id);
        if (dependency != null)
        {
            Console.WriteLine(dependency);
        }
        else
        {
            throw new Exception($"אובייקט מסוג Dependency עם ID {id} לא קיים");
        }
    }//prints the dependency by it's id

    static void readAllDependencies()
    {
        List<DO.Dependency> dependencies = s_dalDependency.ReadAll();
        if (dependencies == null)
        {
            throw new Exception("הרשימה אינה קיימת");
        }
        foreach (DO.Dependency dependency in dependencies)
        {
            Console.WriteLine(dependency);
        }
    }//prits the all list of dependencies

    static void updateDependency()
    {
        int id;
        Console.WriteLine("הכנס מספר מזהה של התלות שברצונך לעדכן:");
        int.TryParse(Console.ReadLine(), out id);
        DO.Dependency? dependency = s_dalDependency.Read(id);
        if (dependency != null)
        {
            Console.WriteLine(dependency);
            DO.Dependency updatedDependency = ReadDependencyFromUser();
            FieldInfo[] fields = updatedDependency.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            // Loop through each field
            foreach (FieldInfo field in fields)
            {
                //string fieldName = field.Name;
                object? fieldValue = field.GetValue(updatedDependency);
                if (fieldValue == null|| fieldValue=="") {
                    field.SetValue(updatedDependency, field.GetValue(dependency));
                }
            }
            updatedDependency.Id= id;//update the new dependency's id to be the same as the task the user want to update
            s_dalDependency.Update(updatedDependency);
            Console.WriteLine(s_dalDependency.Read(id));//prints the updated value
        }
    }//update a dependency task by the user's input

    static void deleteDependency()
    {
        int id;
        Console.WriteLine("הכנס מספר מזהה של התלות שברצונך למחוק:");
        int.TryParse(Console.ReadLine(), out id);
        s_dalDependency.Delete(id);
        Console.WriteLine("האובייקט נמחק בהצלחה");

    }//delete a dependency by it's id from the user's input
    static Engineer ReadEngineerFromUser()
    {
        Console.WriteLine("Enter Engineer Data:");
        // Input fields
        Console.Write("ID: ");
        int id;
        int.TryParse(Console.ReadLine(), out id);
        Console.Write("Name: ");
        string? name = Console.ReadLine();
        Console.Write("Email: ");
        string? email = Console.ReadLine();
        Console.WriteLine("Choose Engineer Experience Level:");
        Console.WriteLine("1. Expert");
        Console.WriteLine("2. Novice");
        Console.WriteLine("3. AdvancedBeginner");
        Console.WriteLine("4. Competent");
        Console.WriteLine("5. Proficient");
        Console.Write("Enter the corresponding number: ");

        EngineerExperience? complexityLevel;
        if (int.TryParse(Console.ReadLine(), out int levelChoice))
        {


            switch (levelChoice)
            {
                case 1:
                    complexityLevel = EngineerExperience.Expert;
                    break;
                case 2:
                    complexityLevel = EngineerExperience.Novice;
                    break;
                case 3:
                    complexityLevel = EngineerExperience.AdvancedBeginner;
                    break;
                case 4:
                    complexityLevel = EngineerExperience.Competent;
                    break;
                case 5:
                    complexityLevel = EngineerExperience.Proficient;
                    break;
                default:
                    complexityLevel=null;
                    break;
            
            }
        }
        else
        {
            complexityLevel = null;
        }

        // Create and return a new Engineer object
        return new Engineer
        {
            Id = id,
            Name = name,
            Email = email,
            Level = complexityLevel
        };
    }//reads the engineer values from the user
    static Dependency ReadDependencyFromUser()
    {
        Console.WriteLine("Enter Dependency Data:");

        // Input fields     
        Console.Write("Dependent Task ID: ");
        int dependentTask1;
        //the TryParse method cant read into a nullable variable,the variablea should be nullables for the case of update
        //Use the same approach as mentioned above for all data intake functions in the program class
        int? dependentTask=null;
        if (int.TryParse(Console.ReadLine(), out dependentTask1))
        {
            dependentTask = dependentTask1;
        }

        Console.Write("Depends On Task ID: ");
        int dependsOnTask1;
        int? dependsOnTask=null;
        if (int.TryParse(Console.ReadLine(), out dependsOnTask1))
        {
            dependsOnTask = dependsOnTask1;
        }

        // Create and return a new Dependency object
        return  new Dependency
        {
           
            DependentTask = dependentTask,
            DependsOnTask = dependsOnTask
        };

    }//reads the dependency values from the user

}
