select 
	-- Content
	c."Id",
	s."Id" as "SubjectId",
	s."Name" as "SubjectName",
	c."LikeCount",
	c."Title",
	c."Text",
	c."Domain",
	c."CreationTime",
	(CASE 
		WHEN l."Id" IS NOT NULL 
			THEN TRUE 
		ELSE FALSE END) 
	as "Liked",

	-- User
	c."UserId",
	u."FirstName",
	u."LastName",

	-- File
	f."Id" as "FileId",
	f."Name",
	f."Extension",
	f."Size",
	f."Path"
from "Content" c
inner join "AspNetUsers" u on u."Id" = c."UserId"
inner join "Subjects" s on s."Id" = c."SubjectId"
left join "Likes" l on l."ContentId" = c."Id" and l."UserId" = @observerId
left join "Files" f on f."OwnerId" = c."Id" and f."DeleteTime" is null and f."OwnerType" = @ownerType
/**where**/
/**orderby**/
limit @pageSize
offset @offset