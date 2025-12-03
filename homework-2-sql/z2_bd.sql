SELECT department_id
FROM Employees
GROUP BY department_id
HAVING COUNT(DISTINCT job_id) > 1;

SELECT manager_id
FROM Employees
GROUP BY manager_id
HAVING 
    COUNT(*) > 5 
    AND SUM(salary) > 50000;