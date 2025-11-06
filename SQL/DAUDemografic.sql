SELECT
    DATE(S.StartTime) AS Dia,
    U.Country AS Pais,
    COUNT(DISTINCT S.UserID) AS DAU
FROM
    Sessions AS S
INNER JOIN
    users AS U ON S.UserID = U.UserID
GROUP BY
    Dia,
    Pais
ORDER BY
    Pais,
    Dia