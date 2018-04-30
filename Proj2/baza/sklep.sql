CREATE DATABASE Sklep;
GO
USE Sklep 

CREATE TABLE Konto(
ID int PRIMARY KEY,
Login VARCHAR(255),
Haslo VARCHAR(32),
Uprawnienia int);

CREATE TABLE Klient(
ID int PRIMARY KEY,
Imie VARCHAR(255),
Nazwisko VARCHAR(255),
Adres VARCHAR(255),
ID_Konto int FOREIGN KEY REFERENCES Konto(ID));

CREATE TABLE Zamowienia(
ID int PRIMARY KEY,
Status int,
data_zlozenia DATETIME,
ID_Klienta int FOREIGN KEY REFERENCES Klient(ID)
);

CREATE TABLE Produkt(
ID int PRIMARY KEY,
Nazwa VARCHAR(255),
Cena DECIMAL,
Dostepnosc int);

CREATE TABLE zam_prod(
ID_Zamowienia int FOREIGN KEY REFERENCES Zamowienia(ID),
ID_Produkt int FOREIGN KEY REFERENCES Produkt(ID),
Ilosc int,
PRIMARY KEY(ID_Zamowienia, ID_Produkt));

CREATE TABLE Pracownik(
ID int PRIMARY KEY,
Imie VARCHAR(255),
Nazwisko VARCHAR(255),
Data_zaczecia DATETIME,
Stanowisko VARCHAR(255),
ID_Konto int FOREIGN KEY REFERENCES Konto(ID));

