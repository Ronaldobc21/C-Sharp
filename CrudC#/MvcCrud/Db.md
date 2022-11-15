CREATE TABLE Usuarios(
    Id INT identity(1,1) PRIMARY KEY,
    Nombre VARCHAR(50),
    Fecha DATE,
    Clave VARCHAR(50)
)

SELECT * FROM Usuarios