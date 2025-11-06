SELECT
    AVG(TIMESTAMPDIFF(SECOND, StartTime, EndTime)) AS average_session_length_seconds
FROM
    Sessions