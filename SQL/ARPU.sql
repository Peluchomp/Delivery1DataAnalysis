WITH TotalRevenue AS (
    SELECT
        SUM(I.Price) AS TotalSumRevenue
    FROM Revenues AS R
    JOIN Items AS I ON R.ItemID = I.ItemID
),
TotalUsers AS (
    SELECT
        COUNT(DISTINCT UserID) AS TotalUserCount
    FROM users
)
SELECT
    TR.TotalSumRevenue / NULLIF(TU.TotalUserCount, 0) AS ARPU
FROM TotalRevenue AS TR, TotalUsers AS TU;