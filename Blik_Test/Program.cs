
using Blik_Test;
using Blik_Test.BO;
using Blik_Test.DAL;
using Blik_Test.Models;
using System.Linq;

#region Data to work with


var hobbies = new List<Hobby>
{
    new Hobby(1, "Music"),
    new Hobby(2, "Sleeping"),
    new Hobby(3, "Animation"),
    new Hobby(4, "Video Games"),
    new Hobby(5, "Cooking")
};

int id = 1;

var people = new List<Person>
{
    new Person(id++, "Jimmy Lee", Enums.Gender.Female, HelperMethods.GenerateHobbiesList(hobbies)),
    new Person(id++, "Kathy Shure", Enums.Gender.Male, HelperMethods.GenerateHobbiesList(hobbies)),
    new Person(id++, "Ellen Mark", Enums.Gender.Other, HelperMethods.GenerateHobbiesList(hobbies)),
    new Person(id++, "Maya Agda", Enums.Gender.Female, HelperMethods.GenerateHobbiesList(hobbies)),
    new Person(id++, "Jen Hoper", Enums.Gender.Female, HelperMethods.GenerateHobbiesList(hobbies)),
    new Person(id++, "John Wick", Enums.Gender.Male, HelperMethods.GenerateHobbiesList(hobbies)),
    new Person(id++, "Jonathan Calm", Enums.Gender.Male, HelperMethods.GenerateHobbiesList(hobbies)),
    new Person(id++, "Natasha Bloom'", Enums.Gender.Female, HelperMethods.GenerateHobbiesList(hobbies)),
    new Person(id++, "Noel Bow", Enums.Gender.Male, HelperMethods.GenerateHobbiesList(hobbies)),
    new Person(id++, "Noel Bow111", (Enums.Gender)1, HelperMethods.GenerateHobbiesList(hobbies))
};

#endregion Data to work with [end]




// Displaying Data:
people.ForEach(p => Console.WriteLine(p));

// TODO list:

// 1. Find all people with the hobby 'Music' and put it into a list
// your code:
Console.WriteLine("___________________________");
Console.WriteLine("people with Music Hobby");

List<Person> peopleMusicHobby = HelperMethods.FilterPersonsBySingleHobby(people, "Music");


peopleMusicHobby.ForEach(m => Console.WriteLine(m));


Console.WriteLine("___________________________");
Console.WriteLine("___________________________");
Console.WriteLine("people with Cooking Hobby");

// 2. Find all people with the hobby 'Cooking' and put it into a list
// your code:
var peopleCookingHobby = HelperMethods.FilterPersonsBySingleHobby(people, "Cooking");

peopleCookingHobby.ForEach(m => Console.WriteLine(m));


Console.WriteLine("___________________________");
Console.WriteLine("___________________________");
Console.WriteLine("people with Music And Cooking Hobbies");
// 3. Get all people with both hobbies - 'Music' and 'Cooking'
// your code:
var peopleMusicAndCookingHobby = HelperMethods.FilterPersonsByTwoHobbies(people, "Music", "Cooking");
peopleMusicAndCookingHobby.ForEach(m => Console.WriteLine(m));
Console.WriteLine("___________________________");


// 4. Create a database and create the needed tables to represent the data of people and hobbies
//    note: People have hobbies.
// Please supply the SQL script for creating those tables with data.

//** The SQL script should be in an SQL file attached to the project (in scripts folder).**//

// 5. Implement a data access layer (DAL) to retrieve the data from the database.
//    Please use Stored Procedures and add them to the SQL script file.

//*** DAL is implemented in DataRetriever class (in DAL folder) ***///

//-- demonstrating usage of DAL----//
//1) getting list of all pepole from db and  filter it in code
var peopleFromdbMusicAndCookingHobby = HelperMethods.FilterPersonsByTwoHobbies(DataRetriever.GetPepole(), "Music", "Cooking");
Console.WriteLine("people with Music And Cooking Hobbies from db - filtered by code");
peopleFromdbMusicAndCookingHobby.ForEach(m => Console.WriteLine(m));
Console.WriteLine("___________________________");
Console.WriteLine("___________________________");
//2) getting filerd list of  pepole from db 
peopleFromdbMusicAndCookingHobby = DataRetriever.FindPersonsByHobbiesName("Music,Cooking");
Console.WriteLine("people with Music And Cooking Hobbies from db - filtered in db");
peopleFromdbMusicAndCookingHobby.ForEach(m => Console.WriteLine(m));
Console.WriteLine("___________________________");
Console.WriteLine("___________________________");

// 6. Take a look at class Department.
//    According to the code we are not allowed to be in a negative balance.
//    Is the code OK? When you run the code - does it behave accordingly?

//--my answer--//
//-- to code is not ok since 2 threads can withdraw sallary and make it negative - both of them will pass the check if a Withdrawal is allowed (_salary >= amount) and then (when the Withdrawal is allowed)
//-- one of the threads will subtract the withdrawn amount from the salary and the other will do the same 
//-- for example salary =110,thread_1 "wants" to withdraw 60 and thread_2 "wants" to withdraw 100
//--thread_1 "checks" sallray and finds that the Withdrawal is allowed (salary>60)
//--thread_2 "checks" sallray and finds that the Withdrawal is allowed (salary>100)
// thread_1 subtract the withdrawn amount from  salary and update it to 50 (110-60)
// thread_2 subtract the withdrawn amount from  salary and update it to -50 (50-100)- since thread_1 already updated the salary

//    If there is a problem in the code - how would you fix it?
//--my answer--//
//--  option 1 : i would add a lock inside Withdraw function to lock the salary update 
//-- option 2: i would add the following annotation to the withdraw function : [MethodImpl(MethodImplOptions.Synchronized)] 
// --  i showed both options in the Department class (option 2 code is commented),plese take a look there
var dep = new Department(1000);
Parallel.For(0, 10, (i) => dep.DoTransactions());










