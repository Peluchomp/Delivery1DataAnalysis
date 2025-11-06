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
  
  SUM(COALESCE(r.total_user_revenue, 0)) AS cohort_total_revenue,
  
  COUNT(DISTINCT r.UserID) AS cohort_paying_users,

  (
    SUM(COALESCE(r.total_user_revenue, 0)) / 
    NULLIF(COUNT(DISTINCT r.UserID), 0)
  ) AS cohort_arppu
FROM
  UserFirstVisit AS f
  LEFT JOIN UserTotalRevenue AS r ON f.UserID = r.UserID
GROUP BY
  f.first_visit_date
ORDER BY
  cohort_date DESC;