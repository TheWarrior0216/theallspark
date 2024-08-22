CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) UNIQUE COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8mb4 COMMENT '';


CREATE TABLE recipe(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  title VARCHAR(255) NOT NULL ,
  instructions VARCHAR(5000) NOT NULL,
  img VARCHAR(1000) NOT NULL,
  category ENUM("breakfast","lunch","dinner", "snack", "dessert") NOT NULL DEFAULT "snack",
  creatorId VARCHAR(255) NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
)

DROP TABLE ingredient

CREATE TABLE ingredient(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name VARCHAR(255) NOT NULL,
  quantity VARCHAR(255) NOT NULL,
  recipeId INT NOT NULL
)

CREATE TABLE favorite(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  recipeId INT NOT NULL,
  accountId varchar(255) NOT NULL,
  FOREIGN KEY (recipeId) REFERENCES recipe (id) ON DELETE CASCADE,
  FOREIGN KEY (accountId) REFERENCES accounts (id) ON DELETE CASCADE,
  UNIQUE (recipeId, accountId)
)

Insert Into
    recipe (
        title,
        instruction,
        img,
        category,
        creatorId
    )
VALUES (
        'Hello',
        'somestuff',
        'idk',
        'breakfast',
        'idk'
    );

SELECT *
FROM ingredient
WHERE id = LAST_INSERT_ID();