

INSERT INTO FILE_MASTER (FILE_NAME, FILE_CREATION_TS, RECORD_CREATED_BY) VALUES('FILE123.TXT', CURRENT_TIMESTAMP, 'SYSTEM');
INSERT INTO FILE_MASTER (FILE_NAME, FILE_CREATION_TS, RECORD_CREATED_BY) VALUES('FILE124.TXT', CURRENT_TIMESTAMP, 'SYSTEM');
INSERT INTO FILE_MASTER (FILE_NAME, FILE_CREATION_TS, RECORD_CREATED_BY) VALUES('FILE125.TXT', CURRENT_TIMESTAMP, 'SYSTEM');
INSERT INTO FILE_MASTER (FILE_NAME, FILE_CREATION_TS, RECORD_CREATED_BY) VALUES('FILE126.TXT', CURRENT_TIMESTAMP, 'SYSTEM');
INSERT INTO FILE_MASTER (FILE_NAME, FILE_CREATION_TS, RECORD_CREATED_BY) VALUES('FILE127.TXT', CURRENT_TIMESTAMP, 'SYSTEM');
COMMIT;

SELECT * FROM FILE_MASTER;

SELECT * FROM FILE_CONTENT;




declare
    l_batchid FILE_MASTER.id%TYPE;
begin
    insert_file_master(v_filename => 'Batch 1',
        v_file_creation => '09-FEB-22 02.12.49.245000000 PM',
        v_record_by => 'SYSTEM',
        v_file_id => l_batchid);
    dbms_output.put_line('Generated id: ' || l_batchid);

    insert_file_master(v_filename => 'Batch 2',
        v_file_creation => '09-FEB-22 02.12.49.245000000 PM',
        v_record_by => 'SYSTEM',
        v_file_id => l_batchid);
    dbms_output.put_line('Generated id: ' || l_batchid);
end;

--DELETE FROM FILE_MASTER;

