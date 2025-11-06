WITH
  UserFirstVisit AS (
    SELECT
      UserID,
      CAST(MIN(StartTime) AS DATE) AS first_visit_date
    FROM
      Sessions
    GROUP BY
      UserID
  ),

  RetainedUsersD1 AS (
    SELECT DISTINCT
      f.UserID,
      f.first_visit_date
    FROM
      UserFirstVisit AS f
      JOIN Sessions AS s ON f.UserID = s.UserID
    WHERE
      CAST(s.StartTime AS DATE) = DATE_ADD(f.first_visit_date, INTERVAL 1 DAY)
  ),

  RetainedUsersD3 AS (
    SELECT DISTINCT
      f.UserID,
      f.first_visit_date
    FROM
      UserFirstVisit AS f
      JOIN Sessions AS s ON f.UserID = s.UserID
    WHERE
      CAST(s.StartTime AS DATE) = DATE_ADD(f.first_visit_date, INTERVAL 3 DAY)
  ),

  RetainedUsersD7 AS (
    SELECT DISTINCT
      f.UserID,
      f.first_visit_date
    FROM
      UserFirstVisit AS f
      JOIN Sessions AS s ON f.UserID = s.UserID
    WHERE
      CAST(s.StartTime AS DATE) = DATE_ADD(f.first_visit_date, INTERVAL 7 DAY)
  )

SELECT
  f.first_visit_date AS cohort_date,
  COUNT(DISTINCT f.UserID) AS cohort_size,

  COUNT(DISTINCT r1.UserID) AS retained_users_d1,
  (COUNT(DISTINCT r1.UserID) * 100.0 / COUNT(DISTINCT f.UserID)) AS d1_retention_percentage,

  COUNT(DISTINCT r3.UserID) AS retained_users_d3,
  (COUNT(DISTINCT r3.UserID) * 100.0 / COUNT(DISTINCT f.UserID)) AS d3_retention_percentage,

  COUNT(DISTINCT r7.UserID) AS retained_users_d7,
  (COUNT(DISTINCT r7.UserID) * 100.0 / COUNT(DISTINCT f.UserID)) AS d7_retention_percentage
  
FROM
  UserFirstVisit AS f
  LEFT JOIN RetainedUsersD1 AS r1 ON f.UserID = r1.UserID
  LEFT JOIN RetainedUsersD3 AS r3 ON f.UserID = r3.UserID
  LEFT JOIN RetainedUsersD7 AS r7 ON f.UserID = r7.UserID
GROUP BY
  f.first_visit_date
ORDER BY
  cohort_date DESC;