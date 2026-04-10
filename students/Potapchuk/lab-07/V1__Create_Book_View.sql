-- Используем обычное VIEW, чтобы избежать ошибок парсера MATERIALIZED
CREATE VIEW book_view AS
SELECT 
    id,
    title,
    current_page,
    total_pages,
    -- Используем CASE вместо прямого сравнения для совместимости
    CASE 
        WHEN current_page = total_pages THEN 1 
        ELSE 0 
    END AS is_completed
FROM books;