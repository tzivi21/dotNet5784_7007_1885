using BO;
using DO;
using System.Reflection;

namespace BalTest;


class Program
{
    static readonly BIApi.IBl s_bl = BIApi.Factory.Get();
    static void Main()
    {
        Console.Write("Would you like to create Initial data? (Y/N)");
        string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input");
        if (ans == "Y")
            DalTest.Initialization.Do();

    }
    #region Menues
    static void mainMenu()
    {
        while (true)
        {
            Console.WriteLine("תפריט ראשי - בחר ישות שברצונך לבדוק:");
            Console.WriteLine("0. יציאה מתפריט ראשי");
            Console.WriteLine("1. משימה");
            Console.WriteLine("2. אבן דרך");
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
                        mileStoneMenu();
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
                try
                {
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
    static void mileStoneMenu()
    {
        bool exitRequested = false;

        while (!exitRequested)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Read Milestone");
            Console.WriteLine("2. Update Milestone");
            Console.WriteLine("3. Exit");

            Console.Write("Enter your choice: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                //לוהיסף אפשרות ללוז הפרויקט
                case "1":

                    break;
                case "2":

                    break;
                case "3":
                    exitRequested = true;
                    Console.WriteLine("Exiting...");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter a valid option.");
                    break;
            }
        }
    }//Manages the milestone menu
    #endregion
    #region TaskActions
    static void createTask()
    {
        int id;
        BO.Task new_task = GetNewTaskFromUserInput();
        if (new_task != null)
        {
            try
            {
                id = s_bl!.Task.Create(new_task);
                Console.WriteLine($"המשימה נוצרה בהצלחה, המספר המזהה הוא: {id}");
            }
            catch (Exception e) { Console.WriteLine(e.Message); }

        }
    }//create a new task
    static void readTask()
    {
        int id;
        Console.WriteLine("הכנס מספר מזהה של המשימה שתרצה לראות:");
        int.TryParse(Console.ReadLine(), out id);
        BO.Task? task = new BO.Task();
        try
        {
            task = s_bl?.Task.Read(id);
        }
        catch (Exception e) { Console.WriteLine(e.Message, e); }
        Console.WriteLine(task);
    }//prints the task by it's id
    static void readAllTasks()
    {
        try
        {
            List<BO.Task> tasks = s_bl!.Task.ReadAll().ToList();
            foreach (BO.Task? task in tasks)
            {
                Console.WriteLine(task);
            }
        }
        catch (Exception e) { Console.WriteLine(e.Message); }


    }//prints the all list of tasks
    static void updateTask()
    {
        int id;
        Console.WriteLine("הכנס מספר מזהה של המשימה שברצונך לעדכן:");
        int.TryParse(Console.ReadLine(), out id);
        try
        {
            BO.Task? task = s_bl?.Task.Read(id);

            Console.WriteLine(task);
            BO.Task updatedTask = GetNewTaskFromUserInput();
            FieldInfo[] fields = updatedTask.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            // Loop through each field
            foreach (FieldInfo field in fields)
            {

                object? fieldValue = field.GetValue(updatedTask);
                if (fieldValue == null || DateTime.Equals(fieldValue, DateTime.MinValue) || fieldValue.ToString() == "")
                {
                    field.SetValue(updatedTask, field.GetValue(task));
                }
            }
            updatedTask.Id = id;//update the new task's id to be the same as the task the user want to update
            s_bl!.Task.Update(updatedTask);
            Console.WriteLine(s_bl.Task.Read(id));//prints the task after the update

        }
        catch (Exception e) { Console.WriteLine(e.Message); }



    }//update a specific task by the user's input
    static void deleteTask()
    {
        int id;
        Console.WriteLine("הכנס מספר מזהה של המשימה שברצונך למחוק:");
        int.TryParse(Console.ReadLine(), out id);
        try
        {
            s_bl!.Task.Delete(id);
            Console.WriteLine("האובייקט נמחק בהצלחה");
        }
        catch (Exception e) { Console.WriteLine(e.Message, e); }

    }//delete a task by it's id from the user's input
    static BO.Task GetNewTaskFromUserInput()
    {
        BO.Task newTask = new BO.Task();

        Console.Write("Enter Description: ");
        newTask.Description = Console.ReadLine();

        Console.Write("Enter Alias: ");
        newTask.Alias = Console.ReadLine();


        Console.WriteLine("Choose Task Status:");
        Console.WriteLine("1. Unscheduled");
        Console.WriteLine("2. Scheduled");
        Console.WriteLine("3. OnTrack");
        Console.WriteLine("4. InJeopardy");
        Console.WriteLine("5. Done");

        bool isValidInput = false;
        while (!isValidInput)
        {
            Console.Write("Enter the number corresponding to the status: ");
            if (int.TryParse(Console.ReadLine(), out int userInput))
            {
                if (Enum.IsDefined(typeof(Status), userInput - 1))
                {
                    newTask.Status = (Status)(userInput - 1);
                    isValidInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }

        Console.Write("Enter Start Date (YYYY-MM-DD HH:MM:SS): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
        {
            newTask.StartDate = startDate;
        }
        else
        {
            Console.WriteLine("Invalid input for Start Date. Defaulting to null.");
            newTask.StartDate = null;
        }

        Console.Write("Enter Scheduled Start Date (YYYY-MM-DD HH:MM:SS): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime scheduledStartDate))
        {
            newTask.ScheduledStartDate = scheduledStartDate;
        }
        else
        {
            Console.WriteLine("Invalid input for Scheduled Start Date. Defaulting to null.");
            newTask.ScheduledStartDate = DateTime.Now;
        }

        Console.Write("Enter Forecast Date (YYYY-MM-DD HH:MM:SS): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime forecastDate))
        {
            newTask.ForeCastDate = forecastDate;
        }
        else
        {
            Console.WriteLine("Invalid input for Forecast Date. Defaulting to minimum date.");
            newTask.ForeCastDate = DateTime.MinValue;
        }

        Console.Write("Enter Deadline Date (YYYY-MM-DD HH:MM:SS): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime deadlineDate))
        {
            newTask.DeadlineDate = deadlineDate;
        }
        else
        {
            Console.WriteLine("Invalid input for Deadline Date. Defaulting to null.");
            newTask.DeadlineDate = null;
        }

        Console.Write("Enter Complete Date (YYYY-MM-DD HH:MM:SS): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime completeDate))
        {
            newTask.CompleteDate = completeDate;
        }
        else
        {
            Console.WriteLine("Invalid input for Complete Date. Defaulting to null.");
            newTask.CompleteDate = null;
        }

        Console.Write("Enter Deliverables: ");
        newTask.Deliverables = Console.ReadLine();

        Console.Write("Enter Remarks: ");
        newTask.Remarks = Console.ReadLine();


        return newTask;
    }

    #endregion
    #region EngineerActions
    static void createEngineer()
    {
        try {
            BO.Engineer newEngineer = GetNewEngineerFromUserInput();
            if (newEngineer != null)
                s_bl!.Engineer.Add(newEngineer);
        }catch (Exception ex) { Console.WriteLine(ex.Message); }
        
    }//create a new engineer

    static void readEngineer()
    {
        int id;
        Console.WriteLine("הכנס מספר מזהה של המהנדס שתרצה לראות:");
        int.TryParse(Console.ReadLine(), out id);
        try
        {
            BO.Engineer? engineer = s_bl!.Engineer.Read(id);

            Console.WriteLine(engineer);
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }


    }//prints the engineer by it's id

    static void readAllEngineers()
    {
        try
        {
            List<BO.Engineer> engineers = s_bl!.Engineer.ReadAll().ToList();

            foreach (BO.Engineer? engineer in engineers)
            {
                Console.WriteLine(engineer);
            }
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }

    }//prints the all list of engineers

    static void updateEngineer()
    {
        int id;
        Console.WriteLine("הכנס מספר מזהה של המהנדס שברצונך לעדכן:");
        int.TryParse(Console.ReadLine(), out id);
        try
        {
            BO.Engineer? engineer = s_bl!.Engineer.Read(id);

            Console.WriteLine(engineer);
            BO.Engineer updatedEngineer = GetNewEngineerFromUserInput();
            FieldInfo[] fields = updatedEngineer.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            // Loop through each field
            foreach (FieldInfo field in fields)
            {
                //string fieldName = field.Name;
                object? fieldValue = field.GetValue(updatedEngineer);
                if (fieldValue == null || fieldValue.ToString() == "")
                {
                    field.SetValue(updatedEngineer, field.GetValue(engineer));
                }
            }
            s_bl.Engineer.Update(updatedEngineer);
            Console.WriteLine(s_bl.Engineer.Read(id));//prints the updated engineer

        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }

    }//update a specific engineer by the user's input

    static void deleteEngineer()
    {
        int id;
        Console.WriteLine("הכנס מספר מזהה של המהנדס שברצונך למחוק:");
        int.TryParse(Console.ReadLine(), out id);
        try
        {
            s_bl!.Engineer.Delete(id);
            Console.WriteLine("האובייקט נמחק בהצלחה");
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); }

    }//delete a engineer by it's id from the user's input
    static BO.Engineer GetNewEngineerFromUserInput()
    {
        BO.Engineer newEngineer = new BO.Engineer();

        Console.Write("Enter ID: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            newEngineer.Id = id;
        }
        else
        {
            Console.WriteLine("Invalid input for ID. Defaulting to 0.");
            newEngineer.Id = 0;
        }

        Console.Write("Enter Name: ");
        newEngineer.Name = Console.ReadLine() ?? "";

        Console.Write("Enter Email: ");
        newEngineer.Email = Console.ReadLine() ?? "";

        Console.WriteLine("Choose Engineer Level:");
        Console.WriteLine("1. Novice");
        Console.WriteLine("2. Advanced Beginner");
        Console.WriteLine("3. Competent");
        Console.WriteLine("4. Proficient");
        Console.WriteLine("5. Expert");

        bool isValidInput = false;
        while (!isValidInput)
        {
            Console.Write("Enter the number corresponding to the level: ");
            if (int.TryParse(Console.ReadLine(), out int userInput))
            {
                if (Enum.IsDefined(typeof(DO.EngineerExperience), userInput - 1))
                {
                    newEngineer.Level = (DO.EngineerExperience)(userInput - 1);
                    isValidInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }

        Console.Write("Enter Cost: ");
        if (double.TryParse(Console.ReadLine(), out double cost))
        {
            newEngineer.Cost = cost;
        }
        else
        {
            Console.WriteLine("Invalid input for Cost. Defaulting to 0.");
            newEngineer.Cost = 0;
        }

        return newEngineer;
    }
    //reads the engineer values from the user
    #endregion
    #region MilestoneActions
    static void ReadMilestone()
    {
        Console.Write("Enter Milestone ID to read: ");
        if (int.TryParse(Console.ReadLine(), out int milestoneId))
        {
            try
            {
                var milestone = s_bl.Milestone.Read(milestoneId);
                Console.WriteLine(milestone);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

        }
    }
    static void UpdateMilestone()
    {
        Console.Write("Enter Milestone ID to update: ");
        if (int.TryParse(Console.ReadLine(), out int updateMilestoneId))
        {
            BO.Milestone updatedMileStone = GetNewMilestoneFromUserInput();
            try
            {
                // Update the milestone
                var updatedMilestone = s_bl.Milestone.Update(updatedMileStone);
                // Display updated milestone details
                Console.WriteLine("Milestone updated.");
            }
            catch (Exception ex) { Console.WriteLine(ex.Message + "\n"); }
        }
    }

        static BO.Milestone GetNewMilestoneFromUserInput()
        {
            BO.Milestone newMilestone = new BO.Milestone();

            Console.Write("Enter Description: ");
            newMilestone.Description = Console.ReadLine();

            Console.Write("Enter Alias: ");
            newMilestone.Alias = Console.ReadLine();

            Console.Write("Enter Remarks: ");
            newMilestone.Remarks = Console.ReadLine();
            return newMilestone;
        }



        #endregion
    }
