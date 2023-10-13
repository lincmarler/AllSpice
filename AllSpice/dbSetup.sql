-- Active: 1696955338622@@SG-mo-7901-mysql-master.servers.mongodirector.com@3306@ThanksForTheGlizzy

CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture'
    ) default charset utf8 COMMENT '';

DROP TABLE accounts ;

CREATE TABLE
    IF NOT EXISTS recipes(
        id INT AUTO_INCREMENT NOT NULL PRIMARY KEY,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last update',
        title VARCHAR(255) NOT NULL,
        instructions VARCHAR(500) NOT NULL,
        img VARCHAR(500) NOT NULL,
        category VARCHAR(100) NOT NULL,
        creatorId VARCHAR(255) NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

DROP TABLE recipes ;

INSERT INTO
    recipes(
        title,
        instructions,
        img,
        category,
        creatorId
    )
VALUES (
        'Good Soup',
        'Make soup gooder',
        'https: / / images.unsplash.com / photo -1613844237701 -8 f3664fc2eff ? ixlib = rb -4.0.3 & ixid = M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA % 3 D % 3 D & auto = format & fit = crop & w = 2128 & q = 80',
        'Soup',
        '652874a1403a85e165b964c0 '
    )

INSERT INTO
    recipes(
        title,
        instructions,
        img,
        category,
        creatorId
    )
VALUES (
        'Chicken Wings',
        'Cook full chicken then eat wings',
        'https: / / images.unsplash.com / photo -1588597989061 - b60ad0eefdbf ? ixlib = rb -4.0.3 & ixid = M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA % 3 D % 3 D & auto = format & fit = crop & w = 2069 & q = 80',
        'Poultry',
        '652874a1403a85e165b964c0 '
    )

CREATE TABLE
    IF NOT EXISTS ingredients(
        id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last update',
        name VARCHAR(255) NOT NULL,
        quantity VARCHAR(100) NOT NULL,
        recipeId INT NOT NULL,
        FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

INSERT INTO
    ingredients (name, quantity, recipeId)
VALUES
('Chicken Wings', 5, '2')