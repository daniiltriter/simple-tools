﻿using SimpleTools.Mapper.DI;

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
model.Name.Should().BeNull();
model.Role.Should().Be(actor.Role);
model.Settings.Should().Be(actor.Settings);







