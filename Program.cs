List<Person> list =  new List<Person>(){
  new Person(1, "Jack"),
  new Person(2, "John"),
};
WebApplication app = WebApplication.Create();
app.MapGet("/", ()=>"Hello World!");
app.MapGet("/people" , () => list);

app.MapGet("/person/{id}", (int id)=>{
  Person? person = list.Find(person => person.id == id);
  return person;
});

app.MapPost("/people", (Person person)=>{
  Person newPerson = new Person(list.Last().id +1, person.name);
  list.Add(newPerson);
  return "New Person Added";
});

app.MapDelete("/person/{id}", (int id) => {
  Person? person = list.Find(person => person.id == id);
  if(person == null) {
    return "Person not Found";
  };
  list.Remove(person);
  return "Person Removed";
});

app.MapPut("/person/{id}", (int id, Person person) => {
  int index = list.FindIndex(person => person.id == id);
  if(index < 0) {
    return "Person not Found";
  };
  Person updatedPerson = new Person(id, person.name);
  list.RemoveAt(index);
  list.Insert(index, updatedPerson);
  return "Person updated";
});

app.Run();

record Person (int id, string name);