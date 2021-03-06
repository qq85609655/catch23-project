$PBExportHeader$Custom_DW.sra
forward
global type Custom_DW from Application
end type
global Transaction sqlca
global DynamicDescriptionArea sqlda
global DynamicStagingArea sqlsa
global Error error
global Message message
end forward

global type Custom_DW from Application
string AppName = "Custom_DW"
end type
global Custom_DW Custom_DW

on Custom_DW.create
sqlca=create Transaction
sqlda=create DynamicDescriptionArea
sqlsa=create DynamicStagingArea
error=create Error
message=create Message
end on

on Custom_DW.destroy
destroy(sqlca)
destroy(sqlda)
destroy(sqlsa)
destroy(error)
destroy(message)
end on

event Open;// Profile tongli_his
SQLCA.DBMS = "SNC SQL Native Client(OLE DB)"
SQLCA.LogPass = "qweasd"
SQLCA.ServerName = "192.168.200.1"
SQLCA.LogId = "sa"
SQLCA.AutoCommit = False
SQLCA.DBParm = "Database='tongli_his'"

connect using sqlca;

Open(w_main)
end event
