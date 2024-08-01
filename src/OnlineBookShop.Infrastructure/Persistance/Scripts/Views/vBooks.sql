CREATE VIEW dbo.vBooks
AS
SELECT Books.Id, 
		Books.Title, 
		Books.[Description], 
		Books.PublishedOn, 
		Books.Price, 
		Books.PublisherId,
		(SELECT AVG(NumStars) FROM dbo.Reviews WHERE Books.Id = Reviews.BookId) AS NumStars
FROM dbo.Books