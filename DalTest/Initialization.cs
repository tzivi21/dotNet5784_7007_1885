
namespace DalTest;
using DO;
using DalApi;
using System;
using System.Collections.Generic;

public static class Initialization
{
    private static IDal? s_dal;
    private static readonly Random s_rand= new Random();

    private static void createEngineers()//initialize the engineers list with random values
    {
        //40 names array
        string[] engineers_names =
        {
            "Alice","Bob","Carol","David","Emma","Frank","Grace",
            "Henry","Irene","Jack","Karen","Liam","Megan","Noah",
            "Olivia","Peter","Quin","Rachel","Sam","Taylor","Ulysses",
            "Victoria","William","Xander","Yasmine","Zoe","Aaron",
            "Bella","Charles","Daisy","Eli","Fiona","George",
            "Hannah","Isabella","John","Katherine","Leo","Madison","Natalie"

        };
        int minValue = 200000000;//the min value for the id
        int maxValue = 400000000;//the max value for the id
        string emailAddressEnd = "@gmail.com";//the end of evey email address
        for (int i = 0; i < engineers_names.Length; i++)
        {
            Engineer item = new Engineer();
            item.Id= s_rand.Next(minValue, maxValue); 
            item.Name= engineers_names[i];
            item.Email = engineers_names[i] + emailAddressEnd;
            //picks a randon Experience from the enum 
            EngineerExperience randomExperienceNext = (EngineerExperience)s_rand.Next(Enum.GetValues(typeof(EngineerExperience)).Length);
            item.Level = randomExperienceNext;
            if (s_dal!.Engineer.Read(item.Id)==null)
                s_dal!.Engineer.Create(item);//add the item to the list
            else
                throw new Exception($"אובייקט מסוג Person עם ID {item.Id} כבר קיים");
        }


    }
    private static void createTasks()//initialize the task's list with random values
    {
        //an array of random strings to play with them
        string[] randomShortSentences = new string[]
        {
            "Hello!","Good morning.","How are you?","I am fine.","Nice to meet you.","Thank you.","You're welcome.","Please wait.",
            "Let's go.","Have a nice day.","I love you.","See you later.","It's a cat.","This is a dog.","I am here.",
            "Where are you?","What's up?","Open the door.","Close the window.","Go away.","Come back.","hello people","i love choclate",
            "No problem.","Great job!", "That's cool.", "Yes, please.", "No, thanks.","What's your name?","My name is John.",
            "Call me tomorrow.","This is easy.","I don't know.","I'm sorry.","Let's eat.","Time to go.","Read a book.",
            "Write a letter.","Speak slowly.", "Listen carefully.", "Watch TV.","Turn it on.","Turn it off.","Look at this.",
            "That's mine.","I'm coming.", "Wait a moment.", "Take a break.", "Don't worry.","Happy birthday.","Good night.",
            "Hello!","Good morning.","How are you?","I am fine.","Nice to meet you.","Thank you.","You're welcome.","Please wait.",
            "Let's go.","Have a nice day.","I love you.","See you later.","It's a cat.","This is a dog.","I am here.","Where are you?",
            "What's up?","Open the door.", "Close the window.","Go away.","Come back.","No problem.","Great job!","That's cool.",
            "Yes, please.", "No, thanks.", "What's your name?", "My name is John.","Call me tomorrow.","This is easy.","I don't know.",
            "I'm sorry.","Let's eat.","Time to go.", "Read a book.", "Write a letter.","Speak slowly.","Listen carefully.","Watch TV.",
            "Turn it on.","Turn it off.", "Look at this.","That's mine.","I'm coming.", "Wait a moment.", "Take a break.", "Don't worry.", "Happy birthday.", "Good night."
        };
        Random random = new Random();
        for (int i = 0; i < randomShortSentences.Length; i++)
        {
            Task item =new Task();
            //create a random sentence from two of the short sentences above
       
            item.Description = randomShortSentences[s_rand.Next(randomShortSentences.Length-1)] + randomShortSentences[s_rand.Next(randomShortSentences.Length-1)];
            //create a random alias by randomly picking a sentence from the array above
            item.Alias = randomShortSentences[s_rand.Next(randomShortSentences.Length-1)];
            item.Milestone = false;
            // Define a range of days between the smallest and biggest date
            DateTime biggestDate = DateTime.Now.AddYears(random.Next(1,5));
            DateTime smallestDate = DateTime.Now; //a range of three years until today
            // Generate the start date
            item.Start = smallestDate;
            // Generate the deadline date
            item.DeadLine = biggestDate;
            // Generate the scheduleDate and complete dates within the range
            item.ScheduleDate = smallestDate.AddDays(s_rand.Next((biggestDate - smallestDate).Days));
            DateTime scheduleDateValue = item.ScheduleDate.Value;
            // Calculate the range of days between biggestDate and ScheduleDate
            int daysDifference = (biggestDate - scheduleDateValue).Days;
            // Generate a random number of days within the calculated range and add it to ScheduleDate
            DateTime randomCompleteDate = scheduleDateValue.AddDays(s_rand.Next(daysDifference));
            // Assign the randomly generated date to the item's Complete property
            item.Complete = randomCompleteDate;
            //a random string for deliverables
            item.Deliverables = randomShortSentences[i];
            //a random string for remarks
            item.Remarks = randomShortSentences[randomShortSentences.Length-1];
            item.RequiredEffortTime = TimeSpan.FromTicks(random.Next(1, 10000));
            //get the engineers list to get an random engineer id from it
            List < Engineer? > engineers_list= s_dal!.Engineer.ReadAll().ToList();//get the engineers list in order to get an id that exist
            int randomIndex = s_rand.Next(0, engineers_list.Count-1);
            Engineer randomEngineer = engineers_list[randomIndex];
            item.Engineerid = randomEngineer!.Id;
            EngineerExperience randomExperienceNext = (EngineerExperience)random.Next(Enum.GetValues(typeof(EngineerExperience)).Length);
            item.ComplexityLevel = randomExperienceNext;
            if (s_dal!.Task.Read(item.Id) == null)
                s_dal!.Task!.Create(item);//add the item to the list
            else
                throw new Exception($"אובייקט מסוג Person עם ID {item.Id} כבר קיים");
        }

    }

    private static void createDependencies()//initialize the dependency's list with random values
    {
        for(int i = 0; i < 250; i++) 
        {
            Dependency item= new Dependency();
            List<Task?> tasks_list = s_dal!.Task.ReadAll().ToList();//get the tasks list in order to get a random task id's that exists 
            int randomIndex1 = s_rand.Next(0, tasks_list.Count-1);
            int randomIndex2 = s_rand.Next(0, tasks_list.Count - 1);
            item.DependentTask = tasks_list[randomIndex1].Id;
            item.DependsOnTask = tasks_list[randomIndex2].Id;
            s_dal!.Dependency.Create(item);
        }

    }
    public static void Do()
    {
        s_dal = Factory.Get;
        //createEngineers();
        //createTasks();
        //createDependencies();
    }


}
