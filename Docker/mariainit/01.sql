CREATE TABLE USER (
  id MEDIUMINT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  name VARCHAR(50) NOT NULL,
  surname VARCHAR(50) NOT NULL,
  email VARCHAR(50) NOT NULL,
  status CHAR(1) NOT NULL,
  createdOn TIMESTAMP NOT NULL,
  createdBy MEDIUMINT NULL,
  modifiedOn TIMESTAMP NOT NULL,
  modifiedBy MEDIUMINT NULL,
  passwordhash VARCHAR(100) NOT NULL,
  INDEX (id),
  INDEX (email, passwordhash)
);



CREATE TABLE HISTORY (
  id MEDIUMINT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  ticker VARCHAR(10) NOT NULL,
  user MEDIUMINT NOT NULL,
  timestamp TIMESTAMP NOT NULL,
  status CHAR(1) NOT NULL,
  FOREIGN KEY (user) REFERENCES USER(id),
  INDEX (id),
  INDEX (user, ticker, timestamp)
);


