-- Вывести список всех клиентов из города Минск.
SELECT * FROM customers 
WHERE city='Минск';

-- Вывести названия и цены всех товаров, отсортированных по убыванию цены.
SELECT product_name, price 
FROM products ORDER BY price DESC;

-- Посчитать общее количество клиентов в базе данных.
SELECT COUNT(customer_id) FROM customers;

-- Найти общую сумму всех заказов.
SELECT SUM(total_amount) FROM orders;

-- Вывести список всех заказов с указанием имени клиента и даты заказа.
SELECT customers.customer_name, orders.order_date 
FROM orders JOIN customers ON orders.customer_id = customers.customer_id; 

-- Найти общую сумму потраченных средств для каждого клиента. Вывести имя клиента и общую сумму.
SELECT customers.customer_name, SUM(orders.total_amount) AS sum_amount 
FROM customers JOIN orders ON orders.customer_id = customers.customer_id 
GROUP BY customer_name;

-- Вывести имена клиентов, которые сделали заказ после '2023-10-01'.
SELECT DISTINCT customers.customer_name 
FROM orders JOIN customers ON customers.customer_id = orders.order_id 
WHERE orders.order_date > '2023-10-01';

-- Найти клиентов, общая сумма заказов которых превышает 10000.
SELECT customers.customer_id, customers.customer_name 
FROM customers JOIN orders ON customers.customer_id = orders.customer_id 
GROUP BY customers.customer_id, customers.customer_name 
HAVING SUM(total_amount) > 10000; 

-- Вывести для каждого клиента его заказы, отсортированные по дате, и добавить столбец с номером заказа по порядку для каждого клиента (ранг).
SELECT customers.customer_name, orders.order_id, orders.order_date, orders.total_amount,
    ROW_NUMBER() OVER (PARTITION BY orders.customer_id ORDER BY orders.order_date) AS order_rank
FROM orders JOIN customers ON orders.customer_id = customers.customer_id;

-- Для каждого заказа вывести его дату и дату предыдущего заказа этого же клиента.
SELECT order_id, customer_id, order_date,
    LAG(order_date) OVER (PARTITION BY customer_id ORDER BY order_date) AS previous_order_date
FROM orders;

-- Найти клиентов с одинаковыми именами в одном городе.
SELECT customer_name, city, COUNT(*) as count
FROM customers
GROUP BY customer_name, city
HAVING COUNT(*) > 1;