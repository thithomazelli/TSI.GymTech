-- AccessLogView
CREATE Or Replace 
    -- ALGORITHM = UNDEFINED 
    -- DEFINER = `academiakalina`@`%` 
    -- SQL SECURITY DEFINER
View AccessLogView As
	Select 
		a.AccessLogId as AccessLogId, 
		p.PersonId as PersonId, 
		p.Name as PersonName, 
		p.ProfileType as PersonProfileType, 
		a.AccessType as AccessType, 
		a.MessageDisplayed as MessageDisplayed, 
		a.CreateDate as CreateDate
	From AccessLog a 
	Left Join Person p on (a.PersonId = p.PersonId);

-- TrainingSheetView
Create Or Replace 
    -- ALGORITHM = UNDEFINED 
    -- DEFINER = `academiakalina`@`%` 
    -- SQL SECURITY DEFINER
View TrainingSheetView As
	Select 
		t.TrainingSheetId as TrainingSheetId, 
		t.Name as Name, 
		t.Cycle as Cycle, 
		t.StudentId as StudentId, 
		p.Name as StudentName, 
		t.Model as Model, 
		t.Status as Status, 
		t.Type as Type
	From TrainingSheet t 
	Left Join Person p on (t.StudentId = p.PersonId);

-- EvaluationSheetView
Create Or Replace 
    -- ALGORITHM = UNDEFINED 
    -- DEFINER = `academiakalina`@`%` 
    -- SQL SECURITY DEFINER
View EvaluationSheetView As
	Select 
		e.EvaluationSheetId as EvaluationSheetId, 
		e.Description as Description, 
		e.StudentId as StudentId, 
		p.Name as StudentName, 
		e.Revaluation as Revaluation, 
		e.Status as Status, 
		e.Comments as Comments
	From EvaluationSheet e 
	Left Join Person p on (e.StudentId = p.PersonId);

-- StudentFrequent
Create or Replace
    -- ALGORITHM = UNDEFINED 
    -- DEFINER = `academiakalina`@`%` 
    -- SQL SECURITY DEFINER
View StudentFrequentView As
	Select 
		Distinct P.PersonId as PersonId, 
		P.Name as Name, 
        P.SocialSecurityCard as SocialSecurityCard, 
        P.Status as Status, 
        P.Email as Email
	From Person P Left Join AccessLog A On (P.PersonId = A.PersonId) 
	Where P.ProfileType = 2 And P.Status = 1
	And A.CreateDate >= DATE(NOW()) - INTERVAL 7 DAY;

-- StudentNotFrequent
Create or Replace
    -- ALGORITHM = UNDEFINED 
    -- DEFINER = `academiakalina`@`%` 
    -- SQL SECURITY DEFINER
View StudentNotFrequentView As
	Select 
		Distinct P.PersonId as PersonId, 
		P.Name as Name, 
        P.SocialSecurityCard as SocialSecurityCard, 
        P.Status as Status, 
        P.Email as Email
	From Person P Left Join AccessLog A On (P.PersonId = A.PersonId) 
	Where P.ProfileType = 2 And P.Status = 1
	-- And A.CreateDate > DATE(NOW()) - INTERVAL 7 DAY;
	And  P.PersonId Not In (Select S.PersonId From StudentFrequentView S);


-- Payment
Create Or Replace 
    -- ALGORITHM = UNDEFINED 
    -- DEFINER = `academiakalina`@`%` 
    -- SQL SECURITY DEFINER
View PaymentView As
	Select 
		p.PaymentId as PaymentId, 
		p.Description as Description, 
        s.PersonId as StudentId, 
		s.Name as StudentName, 
		p.PaymentType as PaymentType,
        p.Status as Status,
        p.DatePaymentEstimated as DatePaymentEstimated, 
        p.DatePaymentCompleted as DatePaymentCompleted,
        p.Discount as Discount,
        p.TotalPrice as TotalPrice
	From Payment p
	Left Join Person s on (p.StudentId = s.PersonId);
