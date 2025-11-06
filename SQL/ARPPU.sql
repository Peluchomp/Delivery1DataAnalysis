WITH 
TotalRevenue AS (
    SELECT
        SUM(I.Price) AS TotalSumRevenue
    FROM Revenues AS R
    JOIN Items AS I 
        ON R.ItemID = I.ItemID
),

PayingUsers AS (
    SELECT
        DISTINCT S.UserID
    FROM Revenues AS R
    JOIN Sessions AS S 
        ON R.SessionID = S.SessionID
),

PayingUserCount AS (
    SELECT
        COUNT(UserID) AS TotalPayingUsers
    FROM PayingUsers
)
SELECT
    TR.TotalSumRevenue,
    PC.TotalPayingUsers,
    TR.TotalSumRevenue / NULLIF(PC.TotalPayingUsers, 0) AS ARPPU
FROM 
    TotalRevenue AS TR,
    PayingUserCount AS PC;