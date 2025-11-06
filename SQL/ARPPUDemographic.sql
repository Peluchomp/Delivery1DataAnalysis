SELECT
    R.Country,
    R.TotalRevenue,
    U.TotalPayingUsers,
    R.TotalRevenue / NULLIF(U.TotalPayingUsers, 0) AS ARPPU_for_Country
FROM
    (
        SELECT
            users.Country,
            SUM(I.Price) AS TotalRevenue
        FROM
            Revenues AS RV
        INNER JOIN
            Sessions AS S ON RV.SessionID = S.SessionID
        INNER JOIN
            users ON S.UserID = users.UserID
        INNER JOIN
            Items AS I ON RV.ItemID = I.ItemID
        GROUP BY
            users.Country
    ) AS R
INNER JOIN
    (
        SELECT
            U.Country,
            COUNT(DISTINCT S.UserID) AS TotalPayingUsers
        FROM
            Revenues AS RV
        INNER JOIN
            Sessions AS S ON RV.SessionID = S.SessionID
        INNER JOIN
            users AS U ON S.UserID = U.UserID
        GROUP BY
            U.Country
    ) AS U ON R.Country = U.Country
ORDER BY
    R.Country