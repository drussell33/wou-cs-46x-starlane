INSERT INTO [Keyword] ([name]) VALUES 
	('Sports'),
	('Cards'),
	('Fish'),
	('Tools'),
	('Art'),
	('Models'),
	('Boardgames'),
	('Stamps'),
	('Insects'),
	('Hats'),
	('Shoes'),
	('Games'),
	('Trophies');

INSERT INTO [Follow] (follower,followed) VALUES
	('1','2'),
	('1','3'),
	('1','4'),
	('2','1'),
	('2','3'),
	('2','4'),
	('3','2'),
	('3','4'),
	('4','1'),
	('4','3');


INSERT INTO [Collection](name,visibility,user_id) VALUES
	('Collection1', 1, '1'),
	('My Fish', 1, '1'),
	('My Beer', 1, '1'),
	('My Tools', 1, '1'),
	('My Friends', 1, '1'),
	('Collection2', 1, '2'),
	('My Trophyies', 1, '2'),
	('My Plants', 1, '2'),
	('My Cards', 1, '2'),
	('My Games', 1, '2'),
	('Collection3', 1, '3'),
	('My Stickers', 1, '3'),
	('My Stamps', 1, '3'),
	('My Posters', 1, '3'),
	('My Funco Pops', 1, '3'),
	('Collection4', 1, '4'),
	('My Toenails', 1, '4'),
	('My Furniture', 1, '4'),
	('My statues', 1, '4'),
	('My Vases', 1, '4');


INSERT INTO [Photo](name, user_id) VALUES
	('new beer','1'),
	('new trophy','1'),
	('best plant','1'),
	('newest stuff','2'),
	('Images 3876','2'),
	('WhiteTail Carp','2'),
	('The hulk','3'),
	('This photo has a really long name','3'),
	('CIgar Box','3'),
	('Energy Drink Can','4'),
	('Weird Large Bug','4'),
	('Deer','4'),
	('Other stuff','4'),
	('pictures of things!','4');

INSERT INTO [FriendsWith](user1_id, user2_id) VALUES
	('1','2'),
	('1','3'),
	('2','3'),
	('2','1'),
	('3','2'),
	('3','1'),
	('3','4'),
	('4','3');