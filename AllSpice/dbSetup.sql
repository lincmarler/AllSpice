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
        'https: / / images.unsplash.com / photo -1613844237701 -8 f3664fc2eff ? ixlib = rb -4.0.3 & ixid = M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA % 3 D % 3 D & auto = format & fit = crop & w = 2128 & q = 80'
    )