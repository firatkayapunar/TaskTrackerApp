INSERT INTO dbo.TaskItems 
(Id, Title, Description, DueDate, IsCompleted, UserId, CreatedAt, CreatedBy, IsDeleted)
VALUES

('1001a111-aaaa-aaaa-aaaa-aaaaaaaa0001', 'Fix bug in login page', 'User cannot login under certain conditions', '2025-01-01 10:00:00.000', 1, '6a1c1d18-1f3b-45e2-8c5f-1cf0d066b101', '2025-01-01 12:00:00.000', 'seed-script', 0),
('1001a111-aaaa-aaaa-aaaa-aaaaaaaa0002', 'Implement registration', 'Add email validation and captcha', NULL, 0, '6a1c1d18-1f3b-45e2-8c5f-1cf0d066b101', '2025-01-01 13:00:00.000', 'seed-script', 0),
('1001a111-aaaa-aaaa-aaaa-aaaaaaaa0003', 'Add password hashing', 'Use bcrypt for secure storage', NULL, 0, '6a1c1d18-1f3b-45e2-8c5f-1cf0d066b101', '2025-01-01 14:00:00.000', 'seed-script', 0),
('1001a111-aaaa-aaaa-aaaa-aaaaaaaa0004', 'Build dashboard UI', 'Use Tailwind CSS and responsive layout', NULL, 0, '6a1c1d18-1f3b-45e2-8c5f-1cf0d066b101', '2025-01-01 15:00:00.000', 'seed-script', 0),
('1001a111-aaaa-aaaa-aaaa-aaaaaaaa0005', 'Integrate JWT auth', 'Generate and validate JWT tokens', NULL, 0, '6a1c1d18-1f3b-45e2-8c5f-1cf0d066b101', '2025-01-01 16:00:00.000', 'seed-script', 0),

('1002b222-bbbb-bbbb-bbbb-bbbbbbbb0001', 'Create user profile view', 'Display user info and settings', '2025-01-02 12:00:00.000', 1, 'b42f1477-f40b-43a0-9456-f64b2d281214', '2025-01-02 12:00:00.000', 'seed-script', 0),
('1002b222-bbbb-bbbb-bbbb-bbbbbbbb0002', 'Setup CI/CD pipeline', 'Use GitHub Actions for deployment', NULL, 0, 'b42f1477-f40b-43a0-9456-f64b2d281214', '2025-01-02 13:00:00.000', 'seed-script', 0),
('1002b222-bbbb-bbbb-bbbb-bbbbbbbb0003', 'Add logging service', 'Log all important user actions', '2025-01-02 14:00:00.000', 1, 'b42f1477-f40b-43a0-9456-f64b2d281214', '2025-01-02 14:00:00.000', 'seed-script', 0),
('1002b222-bbbb-bbbb-bbbb-bbbbbbbb0004', 'Test validation rules', 'Ensure all forms have validators', NULL, 0, 'b42f1477-f40b-43a0-9456-f64b2d281214', '2025-01-02 15:00:00.000', 'seed-script', 0),
('1002b222-bbbb-bbbb-bbbb-bbbbbbbb0005', 'Fix UI issues on mobile', 'Mobile layout overflows in Safari', NULL, 0, 'b42f1477-f40b-43a0-9456-f64b2d281214', '2025-01-02 16:00:00.000', 'seed-script', 0);
