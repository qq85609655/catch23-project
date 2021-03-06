$PBExportHeader$u_quotes.sru
forward
global type u_quotes from u_basepage
end type
type uo_quotebar from u_quotebar within u_quotes
end type
type dw_1 from u_dw within u_quotes
end type
type r_1 from Rectangle within u_quotes
end type
end forward

global type u_quotes from u_basepage
event ue_set_quote (string as_symbol)
uo_quotebar uo_quotebar
dw_1 dw_1
r_1 r_1
end type
global u_quotes u_quotes

type variables
//append to buffer
boolean ib_append = false
end variables

forward prototypes
public function integer of_get_quote (string as_symbol)
private function long of_parsetoarray (string as_source, string as_delimiter, ref string as_array[])
public subroutine of_reset ()
end prototypes

public function integer of_get_quote (string as_symbol);//supports multiple symbols separated by ; or ,
//check if string contains ; or ,

integer li_colonpos, li_ctr, li_commapos
long ll_symbol_count
string lsymbols[ ]
ib_append = false
li_colonpos = pos(as_symbol, ';')
li_commapos = pos(as_symbol, ',')
if li_commapos> 0 and li_colonpos>0 then //put up a message invalid chars in string
	of_showmessage(true, "Can't have both ; and , as symbol separators" )
	return -1 
end if
of_showmessage(false,"")
				
if li_commapos = 0 and li_colonpos=0 then return dw_1.retrieve(as_symbol) //just one symbol requested
if li_commapos> 0 then
	ll_symbol_count = of_parsetoarray( as_symbol, ',', lsymbols )
end if	
if li_colonpos> 0 then
	ll_symbol_count = of_parsetoarray( as_symbol, ';', lsymbols )
end if	
for li_ctr = 1 to ll_symbol_count
	dw_1.retrieve(lsymbols[ li_ctr] )	
	ib_append = true
next
end function

private function long of_parsetoarray (string as_source, string as_delimiter, ref string as_array[]);//////////////////////////////////////////////////////////////////////////////
//
//	Function:  of_ParseToArray
//
//	Access:  public
//
//	Arguments:
//	as_Source   The string to parse.
//	as_Delimiter   The delimeter string.
//	as_Array[]   The array to be filled with the parsed strings, passed by reference.
//
//	Returns:  long
//	The number of elements in the array.
//	If as_Source or as_Delimeter is NULL, function returns NULL.
//
//	Description:  Parse a string into array elements using a delimeter string.
//
//////////////////////////////////////////////////////////////////////////////
//
//	Revision History
//
//	Version
//	5.0   Initial version
//	5.0.02   Fixed problem when delimiter is last character of string.

//	   Ref array and return code gave incorrect results.
//
//////////////////////////////////////////////////////////////////////////////
//
/*
 * Open Source PowerBuilder Foundation Class Libraries
 *
 * Copyright (c) 2004-2005, All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted in accordance with the GNU Lesser General
 * Public License Version 2.1, February 1999
 *
 * http://www.gnu.org/copyleft/lesser.html
 *
 * ====================================================================
 *
 * This software consists of voluntary contributions made by many
 * individuals and was originally based on software copyright (c) 
 * 1996-2004 Sybase, Inc. http://www.sybase.com.  For more
 * information on the Open Source PowerBuilder Foundation Class
 * Libraries see http://pfc.codexchange.sybase.com
*/
//
//////////////////////////////////////////////////////////////////////////////

long		ll_DelLen, ll_Pos, ll_Count, ll_Start, ll_Length
string 	ls_holder

//Check for NULL
IF IsNull(as_source) or IsNull(as_delimiter) Then
	long ll_null
	SetNull(ll_null)
	Return ll_null
End If

//Check for at leat one entry
If Trim (as_source) = '' Then
	Return 0
End If

//Get the length of the delimeter
ll_DelLen = Len(as_Delimiter)

ll_Pos =  Pos(Upper(as_source), Upper(as_Delimiter))

//Only one entry was found
if ll_Pos = 0 then
	as_Array[1] = as_source
	return 1
end if

//More than one entry was found - loop to get all of them
ll_Count = 0
ll_Start = 1
Do While ll_Pos > 0
	
	//Set current entry
	ll_Length = ll_Pos - ll_Start
	ls_holder = Mid (as_source, ll_start, ll_length)

	// Update array and counter
	ll_Count ++
	as_Array[ll_Count] = ls_holder
	
	//Set the new starting position
	ll_Start = ll_Pos + ll_DelLen

	ll_Pos =  Pos(Upper(as_source), Upper(as_Delimiter), ll_Start)
Loop

//Set last entry
ls_holder = Mid (as_source, ll_start, Len (as_source))

// Update array and counter if necessary
if Len (ls_holder) > 0 then
	ll_count++
	as_Array[ll_Count] = ls_holder
end if

//Return the number of entries found
Return ll_Count
end function

public subroutine of_reset ();dw_1.Reset( )
end subroutine

on u_quotes.create
int iCurrent
call super::create
this.uo_quotebar = create uo_quotebar
this.dw_1 = create dw_1
this.r_1 = create r_1
iCurrent=UpperBound(this.Control)
this.Control[iCurrent+1]=this.uo_quotebar
this.Control[iCurrent+2]=this.dw_1
this.Control[iCurrent+3]=this.r_1
end on

on u_quotes.destroy
call super::destroy
destroy(this.uo_quotebar)
destroy(this.dw_1)
destroy(this.r_1)
end on

event ue_set_quote;uo_quotebar.dynamic of_set_quote( as_symbol )
end event

event ue_setstate;call super::ue_setstate;dw_1.setwsobject( gn_controller.of_get_wsconn( ) )
end event

type uo_quotebar from u_quotebar within u_quotes
end type

on uo_quotebar.create
call super::create
end on

on uo_quotebar.destroy
call super::destroy
end on

type dw_1 from u_dw within u_quotes
string DataObject = "d_quotes"
end type

on dw_1.create
call super::create
end on

on dw_1.destroy
call super::destroy
end on

event retrievestart;call super::retrievestart;if ib_append = true then 
	return 2		//append			
else
	return 0		//replace
end if
end event

event buttonclicked;call super::buttonclicked;//show the trade stocks uo
u_buy_sell l_page
l_page = w_stocktrader.event ue_show_buy_sell( )
l_page.of_initialize( 'B', 0, 0, &
	this.getitemstring(row, 'symbol'), this.getitemdecimal( row, 'price'))
end event

event Constructor;ib_headersort = true
end event

type r_1 from Rectangle within u_quotes
end type

on r_1.create
end on

on r_1.destroy
end on
