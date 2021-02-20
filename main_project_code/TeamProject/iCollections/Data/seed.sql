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
	('replaceUser1','replaceUser2'),
	('replaceUser1','replaceUser3'),
	('replaceUser1','replaceUser4'),
	('replaceUser1','replaceUser3'),
	('replaceUser2','replaceUser1'),
	('replaceUser2','replaceUser3'),
	('replaceUser3','replaceUser2'),
	('replaceUser3','replaceUser4'),
	('replaceUser4','replaceUser1'),
	('replaceUser4','replaceUser3');


INSERT INTO [Collection](name,visibility,user_id) VALUES
	('Collection1', 1, 'replaceUser1'),
	('My Fish', 1, 'replaceUser1'),
	('My Beer', 1, 'replaceUser1'),
	('My Tools', 1, 'replaceUser1'),
	('My Friends', 1, 'replaceUser1'),
	('Collection2', 1, 'replaceUser2'),
	('My Trophyies', 1, 'replaceUser2'),
	('My Plants', 1, 'replaceUser2'),
	('My Cards', 1, 'replaceUser2'),
	('My Games', 1, 'replaceUser2'),
	('Collection3', 1, 'replaceUser3'),
	('My Stickers', 1, 'replaceUser3'),
	('My Stamps', 1, 'replaceUser3'),
	('My Posters', 1, 'replaceUser3'),
	('My Funco Pops', 1, 'replaceUser3'),
	('Collection4', 1, 'replaceUser4'),
	('My Toenails', 1, 'replaceUser4'),
	('My Furniture', 1, 'replaceUser4'),
	('My statues', 1, 'replaceUser4'),
	('My Vases', 1, 'replaceUser4');


INSERT INTO [Photo](name, user_id) VALUES
	('new beer','replaceUser1'),
	('new trophy','replaceUser1'),
	('best plant','replaceUser1'),
	('newest stuff','replaceUser2'),
	('Images 3876','replaceUser2'),
	('WhiteTail Carp','replaceUser2'),
	('The hulk','replaceUser3'),
	('This photo has a really long name','replaceUser3'),
	('CIgar Box','replaceUser3'),
	('Energy Drink Can','replaceUser4'),
	('Weird Large Bug','replaceUser4'),
	('Deer','replaceUser4'),
	('Other stuff','replaceUser4'),
	('pictures of things!','replaceUser4');

INSERT INTO [FriendsWith](user1_id, user2_id) VALUES
	('replaceUser1','replaceUser2'),
	('replaceUser1','replaceUser3'),
	('replaceUser1','replaceUser4'),
	('replaceUser1','replaceUser3'),
	('replaceUser2','replaceUser1'),
	('replaceUser2','replaceUser3'),
	('replaceUser3','replaceUser2'),
	('replaceUser3','replaceUser4'),
	('replaceUser4','replaceUser1'),
	('replaceUser4','replaceUser3');