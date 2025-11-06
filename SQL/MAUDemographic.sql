SELECT
    A.ReportDate,
    C.Pais,
    COUNT(DISTINCT C.UserID) AS MAU_Rolling30Day
FROM
    (
        SELECT DISTINCT
            DATE(StartTime) AS ReportDate
        FROM Sessions
    ) AS A
JOIN
    (
        SELECT DISTINCT
            DATE(S.StartTime) AS ActivityDate,
            S.UserID,
            U.Country AS Pais
        FROM
            Sessions AS S
        INNER JOIN
            users AS U ON S.UserID = U.UserID
    ) AS C
    ON C.ActivityDate BETWEEN DATE_SUB(A.ReportDate, INTERVAL 29 DAY) AND A.ReportDate
GROUP BY
    A.ReportDate,
    C.Pais
ORDER BY
    Pais,
    A.ReportDate