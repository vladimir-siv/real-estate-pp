--UPDATE [Properties] SET [Name] = 'Ukupan broj spratova' WHERE [Name] LIKE 'Ukupan%spratova'
SELECT COUNT(*) FROM [RealEstates]

SELECT TOP 1 * FROM [RealEstates]
SELECT TOP 1 * FROM [Properties]
SELECT DISTINCT [Name] FROM [Properties] ORDER BY [Name]

SELECT P1.[Value], COUNT(P1.[Value]) FROM [Properties] P1
JOIN [Properties] P2 ON P1.RealEstate_ID = P2.RealEstate_ID
WHERE P1.[Name] = 'Grad' AND P2.[Name] = 'Transakcija' AND P2.[Value] = 'Prodaja'
GROUP BY P1.[Value]

SELECT DISTINCT [Value] FROM [Properties]
WHERE [Name] = 'Kategorija' --'Kategorija Uknjiženo'

SELECT DISTINCT [Value] FROM [Properties]
WHERE [Name] = 'Zemlja'

SELECT DISTINCT [Value] FROM [Properties]
WHERE [Name] = 'Godina izgradnje'