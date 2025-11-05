WITH DAU_Log AS (
    SELECT DISTINCT
        DATE(StartTime) AS ActivityDate,
        UserID
    FROM Sessions
)
SELECT
    d.ActivityDate AS ReportDate,
    COUNT(DISTINCT CASE WHEN u.ActivityDate = d.ActivityDate THEN u.UserID END) AS DAU_Today,
    COUNT(DISTINCT u.UserID) AS MAU_Rolling30Day,
    (COUNT(DISTINCT CASE WHEN u.ActivityDate = d.ActivityDate THEN u.UserID END) * 100.0
        / COUNT(DISTINCT u.UserID)) AS Stickiness_Percentage
FROM (SELECT DISTINCT ActivityDate FROM DAU_Log) d
JOIN DAU_Log u
    ON u.ActivityDate BETWEEN DATE_SUB(d.ActivityDate, INTERVAL 29 DAY) AND d.ActivityDate
GROUP BY
    d.ActivityDate
ORDER BY
    d.ActivityDate DESC;