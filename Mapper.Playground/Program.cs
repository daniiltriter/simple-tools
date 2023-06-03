using SimpleTools.Mapper.DI;

var services = new ServiceCollection()
    .AddMapper(options =>
    {
        options.With<ActorMapConfiguration>();
    })
    .BuildServiceProvider();

// arrange
var actor = Actor.Make(Faker.Name.FullName(), Faker.Enum.Random<ActorRole>());
var settings = new ActorSettings()
{
    Name = Faker.Name.Last(),
    Enabled = Faker.Boolean.Random()
};
actor.Settings = settings;

// act
var mapper = services.GetService<IMapper>();
var model = mapper.Map<Actor, ActorModel>(actor);

// assert
model.Name.Should().Be(actor.Name);
model.Role.Should().Be(actor.Role);
model.Settings.Should().Be(actor.Settings);

//
//
//
//
//
//
// var str = "Hello";
//
// var func = new Func<string, char>(_ => _[0]);
//
// object obj = func;
//
// var res = ((Func<string, char>)obj)(str);
// Console.WriteLine(res);