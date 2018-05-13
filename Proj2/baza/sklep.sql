CREATE DATABASE Sklep;
GO
USE Sklep 

CREATE TABLE Konto(
ID int PRIMARY KEY IDENTITY(1,1),
Login VARCHAR(255),
Class_Login int,
Haslo VARCHAR(32),
Class_Haslo int,
Salt VARCHAR(5),
Class_salt int,
Clear int,
Class int);

CREATE TABLE Klient(
ID int PRIMARY KEY IDENTITY(1,1),
Imie VARCHAR(255),
Class_Imie int,
Nazwisko VARCHAR(255),
Class_Nazwisko int,
Adres VARCHAR(255),
Class_Adres int,
ID_Konto int FOREIGN KEY REFERENCES Konto(ID),
Class int);

CREATE TABLE Zamowienia(
ID int PRIMARY KEY IDENTITY(1,1),
Status int,
Class_Status int,
Data_zlozenia DATETIME,
Class_Data_zlozenia int,
ID_Klienta int FOREIGN KEY REFERENCES Klient(ID),
Class int
);

CREATE TABLE Produkt(
ID int PRIMARY KEY IDENTITY(1,1),
Nazwa VARCHAR(255),
Class_Nazwa int,
Cena DECIMAL,
Class_Cena int,
Dostepnosc int,
Class_Dostepnosc int,
Class int);

CREATE TABLE zam_prod(
ID_Zamowienia int FOREIGN KEY REFERENCES Zamowienia(ID),
ID_Produkt int FOREIGN KEY REFERENCES Produkt(ID),
Ilosc int,
Class_Ilosc int,
PRIMARY KEY(ID_Zamowienia, ID_Produkt),
Class int);

CREATE TABLE Pracownik(
ID int PRIMARY KEY IDENTITY(1,1),
Imie VARCHAR(255),
Class_Imie int,
Nazwisko VARCHAR(255),
Class_Nazwisko int,
Data_zaczecia DATETIME,
Class_Data_zaczecia int,
Stanowisko VARCHAR(255),
Class_Stanowisko int,
ID_Konto int FOREIGN KEY REFERENCES Konto(ID),
Class int);

