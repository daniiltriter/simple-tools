var services = new ServiceCollection()
    .AddMapper()
    .BuildServiceProvider();

// arrange
var actor = Actor.Make(Faker.Name.FullName(), Faker.Enum.Random<ActorRole>());
var settings = new ActorSettings()
{
    Name = Faker.Name.Last(),
    Enabled = Faker.Boolean.Random()
};

// act
var mapper = services.GetService<IMapper>();
var model = mapper.Map<Actor, ActorModel>(actor);

// assert
model.Name.Should().Be(actor.Name);
model.Role.Should().Be(actor.Role);
model.Settings.Should().Be(actor.Settings);





