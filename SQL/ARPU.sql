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
  
  UserTotalRevenue AS (
    SELECT
      s.UserID,
      SUM(r.RevenueAmount) AS total_user_revenue
    FROM
      Revenues AS r
      JOIN Sessions AS s ON r.SessionID = s.SessionID
    GROUP BY
      s.UserID
  )

SELECT
  f.first_visit_date AS cohort_date,
  COUNT(DISTINCT f.UserID) AS cohort_size,
  SUM(COALESCE(r.total_user_revenue, 0)) AS cohort_total_revenue,
  (
    SUM(COALESCE(r.total_user_revenue, 0)) / COUNT(DISTINCT f.UserID)
  ) AS cohort_arpu
FROM
  UserFirstVisit AS f
  LEFT JOIN UserTotalRevenue AS r ON f.UserID = r.UserID
GROUP BY
  f.first_visit_date
ORDER BY
  cohort_date DESC;