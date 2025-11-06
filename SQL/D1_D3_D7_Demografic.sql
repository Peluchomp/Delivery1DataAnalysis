SELECT
    f.first_visit_date AS cohort_date,
    f.Country AS Country, 
    COUNT(DISTINCT f.UserID) AS cohort_size,

    COUNT(DISTINCT s1.UserID) AS retained_users_d1,
    (COUNT(DISTINCT s1.UserID) * 100.0 / COUNT(DISTINCT f.UserID)) AS d1_retention_percentage,

    COUNT(DISTINCT s3.UserID) AS retained_users_d3,
    (COUNT(DISTINCT s3.UserID) * 100.0 / COUNT(DISTINCT f.UserID)) AS d3_retention_percentage,

    COUNT(DISTINCT s7.UserID) AS retained_users_d7,
    (COUNT(DISTINCT s7.UserID) * 100.0 / COUNT(DISTINCT f.UserID)) AS d7_retention_percentage
    
FROM
    (
        SELECT
            S.UserID,
            CAST(MIN(S.StartTime) AS DATE) AS first_visit_date,
            U.Country
        FROM
            Sessions AS S
        INNER JOIN
            users AS U ON S.UserID = U.UserID
        GROUP BY
            S.UserID, U.Country
    ) AS f

LEFT JOIN
    Sessions AS s1 ON f.UserID = s1.UserID
    AND DATE(s1.StartTime) = DATE_ADD(f.first_visit_date, INTERVAL 1 DAY)

LEFT JOIN
    Sessions AS s3 ON f.UserID = s3.UserID
    AND DATE(s3.StartTime) = DATE_ADD(f.first_visit_date, INTERVAL 3 DAY)

LEFT JOIN
    Sessions AS s7 ON f.UserID = s7.UserID
    AND DATE(s7.StartTime) = DATE_ADD(f.first_visit_date, INTERVAL 7 DAY)

GROUP BY
    f.first_visit_date,
    f.Country
ORDER BY
    f.Country,
    cohort_date DESC