CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL PRIMARY KEY COMMENT 'primary key',
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name VARCHAR(255) COMMENT 'User Name',
  email VARCHAR(255) UNIQUE COMMENT 'User Email',
  picture VARCHAR(255) COMMENT 'User Picture'
) default charset utf8mb4 COMMENT '';

CREATE TABLE restaurants(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  name TINYTEXT NOT NULL,
  img_url TEXT NOT NULL,
  description TEXT NOT NULL,
  visits INT UNSIGNED NOT NULL DEFAULT 0,
  is_shutdown BOOLEAN NOT NULL DEFAULT false,
  creator_id VARCHAR(255) NOT NULL,
  FOREIGN KEY(creator_id) REFERENCES accounts(id) ON DELETE CASCADE
);

CREATE TABLE reports(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  title TINYTEXT NOT NULL,
  body TEXT,
  score TINYINT UNSIGNED NOT NULL,
  img_url TEXT,
  creator_id VARCHAR(255) NOT NULL,
  restaurant_id INT NOT NULL,
  FOREIGN KEY(creator_id) REFERENCES accounts(id) ON DELETE CASCADE,
  FOREIGN KEY(restaurant_id) REFERENCES restaurants(id) ON DELETE CASCADE
);

SELECT * FROM reports;

DELETE FROM reports;

DELETE FROM restaurants;
SELECT * FROM restaurants;

SELECT visits FROM restaurants WHERE id = 4;


SELECT
restaurants.*,
COUNT(reports.id) AS report_count,
accounts.*
FROM restaurants
INNER JOIN accounts ON accounts.id = restaurants.creator_id
LEFT OUTER JOIN reports ON reports.restaurant_id = restaurants.id
GROUP BY restaurants.id
ORDER BY restaurants.created_at ASC;