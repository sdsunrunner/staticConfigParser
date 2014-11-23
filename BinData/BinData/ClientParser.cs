/**************************************************************************
 *
 *
 *					此文件为自动生成 不要自行更改!!!
 *
 *
 *************************************************************************/
using System;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;
using System.Text;
using System.IO;
using ProtoBuf;
using clientdata;

namespace BinData
{
class ClientParser
{
// const字符串
public static string strEnd = " \r\n";
public static string strXlsx = ".xlsx";
public static string strDat = ".dat";
public static string NowTime() { return DateTime.Now.ToString() + " "; }

// Excel相关对象
public static Excel.Application xApp;
public static Excel.Workbook xBook;
public static Excel.Worksheet xSheet;
public static int nSheetIndex;

// 打开工作薄
public static void GetBook(string szName)
{
xBook = xApp.Workbooks.Open(szName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
}

// 打开指定Worksheet
public static void GetSheet()
{ xSheet = (Excel.Worksheet)xBook.Sheets[nSheetIndex]; }

private static void NextSheet()
{
++nSheetIndex;
GetSheet();
}

// 行数
public static int Rows()
{ return xSheet.UsedRange.Cells.Rows.Count; }

// 列数
public static int Cols()
{ return xSheet.UsedRange.Cells.Columns.Count; }

// 分割字符串
public static String[] SubString(String strData, char separator)
{
String[] strArray = strData.Split(separator);
return strArray;
}

public static string GetString(Excel.Range range)
{
if (null == range.Value2)
{ return ""; }
return range.Value2.ToString();
}

// 解析整型字段
public static Int32 ReadInt32(int i, int j)
{
Excel.Range range = xSheet.Cells[i, j];
if (null == range.Value2)
{ return 0; }
return System.Convert.ToInt32(range.Value2.ToString());
}

public static Int64 ReadInt64(int i, int j)
{
Excel.Range range = xSheet.Cells[i, j];
if (null == range.Value2)
{ return 0; }
return System.Convert.ToInt64(range.Value2.ToString());
}

// 解析float
public static float ReadFloat(int i, int j)
{
Excel.Range range = xSheet.Cells[i, j];
if (null == range.Value2)
{ return 0; }
return System.Convert.ToSingle(range.Value2.ToString());
}

// 解析字符串
public static byte[] ReadString(int i, int j)
{
Excel.Range range = xSheet.Cells[i, j];
if (null == range.Value2)
{ return Encoding.UTF8.GetBytes(""); }
return Encoding.UTF8.GetBytes(range.Value2.ToString());
}

// 开始解析Excel
private static void StartParse(string path)
{
nSheetIndex = 1;
string strPath = MeFile.GetFilaPath(path);
xApp = new Excel.Application();
GetBook(strPath);
GetSheet();
}

// 结束解析Excel
public static void EndParse()
{
nSheetIndex = 1;
xSheet = null;
xBook = null;
if (null != xApp)
{ xApp.Quit(); }
xApp = null;
}

public static void ParseClientBuffer( string fileName )
{
StartParse(fileName);
clientdata.BufferList xList = new clientdata.BufferList();
int x = 1;
string[] sArray;
// 数据从第5行开始
for (int i = 5; i <= Rows(); ++i)
{
clientdata.BufferData xData = new clientdata.BufferData();
xData.id = ReadInt32( i,  x++ );
xData.type = ReadInt32( i,  x++ );
xData.value = ReadInt32( i,  x++ );
xData.time_length = ReadInt32( i,  x++ );
x = 1;
xList.data.Add(xData);
}
NextSheet();
x = 1;

string fPath = System.Environment.CurrentDirectory + @"\..\..\DataClient\";
FileStream wFile = new FileStream(fPath + fileName + ".dat", FileMode.Create, FileAccess.Write);
Serializer.Serialize<BufferList>(wFile, xList);
wFile.Close();

FileStream rFile = new FileStream(fPath + fileName + ".dat", FileMode.Open, FileAccess.Read);
BufferList readList = Serializer.Deserialize<BufferList>(rFile);
rFile.Close();
EndParse();
}

public static void ParseClientCharacter( string fileName )
{
StartParse(fileName);
clientdata.CharacterList xList = new clientdata.CharacterList();
int x = 1;
string[] sArray;
// 数据从第5行开始
for (int i = 5; i <= Rows(); ++i)
{
clientdata.CharacterData xData = new clientdata.CharacterData();
xData.level = ReadInt32( i,  x++ );
xData.exp = ReadInt32( i,  x++ );
xData.attack = ReadInt32( i,  x++ );
xData.hp = ReadInt32( i,  x++ );
xData.speed = ReadInt32( i,  x++ );
xData.attackspeed = ReadInt32( i,  x++ );
x = 1;
xList.data.Add(xData);
}
NextSheet();
x = 1;

string fPath = System.Environment.CurrentDirectory + @"\..\..\DataClient\";
FileStream wFile = new FileStream(fPath + fileName + ".dat", FileMode.Create, FileAccess.Write);
Serializer.Serialize<CharacterList>(wFile, xList);
wFile.Close();

FileStream rFile = new FileStream(fPath + fileName + ".dat", FileMode.Open, FileAccess.Read);
CharacterList readList = Serializer.Deserialize<CharacterList>(rFile);
rFile.Close();
EndParse();
}

public static void ParseClientCompose( string fileName )
{
StartParse(fileName);
clientdata.ComposeList xList = new clientdata.ComposeList();
int x = 1;
string[] sArray;
// 数据从第5行开始
for (int i = 5; i <= Rows(); ++i)
{
clientdata.ComposeData xData = new clientdata.ComposeData();
xData.itemid = ReadInt32( i,  x++ );
xData.element_first = ReadInt32( i,  x++ );
xData.element_first_num = ReadInt32( i,  x++ );
xData.element_second = ReadInt32( i,  x++ );
xData.element_second_num = ReadInt32( i,  x++ );
xData.element_third = ReadInt32( i,  x++ );
xData.element_third_num = ReadInt32( i,  x++ );
x = 1;
xList.data.Add(xData);
}
NextSheet();
x = 1;

string fPath = System.Environment.CurrentDirectory + @"\..\..\DataClient\";
FileStream wFile = new FileStream(fPath + fileName + ".dat", FileMode.Create, FileAccess.Write);
Serializer.Serialize<ComposeList>(wFile, xList);
wFile.Close();

FileStream rFile = new FileStream(fPath + fileName + ".dat", FileMode.Open, FileAccess.Read);
ComposeList readList = Serializer.Deserialize<ComposeList>(rFile);
rFile.Close();
EndParse();
}

public static void ParseClientDrop( string fileName )
{
StartParse(fileName);
clientdata.DropList xList = new clientdata.DropList();
int x = 1;
string[] sArray;
// 数据从第5行开始
for (int i = 5; i <= Rows(); ++i)
{
clientdata.DropData xData = new clientdata.DropData();
xData.gropid = ReadInt32( i,  x++ );
xData.type = ReadInt32( i,  x++ );
xData.itemid = ReadInt32( i,  x++ );
xData.rate = ReadInt32( i,  x++ );
xData.count = ReadInt32( i,  x++ );
x = 1;
xList.data.Add(xData);
}
NextSheet();
x = 1;

string fPath = System.Environment.CurrentDirectory + @"\..\..\DataClient\";
FileStream wFile = new FileStream(fPath + fileName + ".dat", FileMode.Create, FileAccess.Write);
Serializer.Serialize<DropList>(wFile, xList);
wFile.Close();

FileStream rFile = new FileStream(fPath + fileName + ".dat", FileMode.Open, FileAccess.Read);
DropList readList = Serializer.Deserialize<DropList>(rFile);
rFile.Close();
EndParse();
}

public static void ParseClientEquip( string fileName )
{
StartParse(fileName);
clientdata.EquipList xList = new clientdata.EquipList();
int x = 1;
string[] sArray;
// 数据从第5行开始
for (int i = 5; i <= Rows(); ++i)
{
clientdata.EquipData xData = new clientdata.EquipData();
xData.id = ReadInt32( i,  x++ );
xData.type = ReadInt32( i,  x++ );
xData.group_id = ReadInt32( i,  x++ );
xData.group_index = ReadInt32( i,  x++ );
xData.quality = ReadInt32( i,  x++ );
xData.name = ReadString( i,  x++ );
xData.icon = ReadString( i,  x++ );
xData.view = ReadString( i,  x++ );
xData.skillid = ReadInt32( i,  x++ );
xData.damage = ReadInt32( i,  x++ );
xData.att_space = ReadInt32( i,  x++ );
xData.fallback = ReadInt32( i,  x++ );
xData.move = ReadInt32( i,  x++ );
xData.hp = ReadInt32( i,  x++ );
xData.armor = ReadInt32( i,  x++ );
xData.damage_add = ReadInt32( i,  x++ );
xData.hp_add = ReadInt32( i,  x++ );
xData.att_speed_add = ReadInt32( i,  x++ );
xData.move_add = ReadInt32( i,  x++ );
xData.crit = ReadInt32( i,  x++ );
xData.crit_dmg = ReadInt32( i,  x++ );
xData.desc = ReadString( i,  x++ );
xData.strenthstuff_first = ReadInt32( i,  x++ );
xData.strenthstuff_second = ReadInt32( i,  x++ );
xData.strenthstuff_third = ReadInt32( i,  x++ );
xData.strenthstuff_fourth = ReadInt32( i,  x++ );
xData.strenthstuff_fifth = ReadInt32( i,  x++ );
xData.strenthstuff_sixth = ReadInt32( i,  x++ );
x = 1;
xList.data.Add(xData);
}
NextSheet();
x = 1;

string fPath = System.Environment.CurrentDirectory + @"\..\..\DataClient\";
FileStream wFile = new FileStream(fPath + fileName + ".dat", FileMode.Create, FileAccess.Write);
Serializer.Serialize<EquipList>(wFile, xList);
wFile.Close();

FileStream rFile = new FileStream(fPath + fileName + ".dat", FileMode.Open, FileAccess.Read);
EquipList readList = Serializer.Deserialize<EquipList>(rFile);
rFile.Close();
EndParse();
}

public static void ParseClientEquipGroup( string fileName )
{
StartParse(fileName);
clientdata.EquipGroupList xList = new clientdata.EquipGroupList();
int x = 1;
string[] sArray;
// 数据从第5行开始
for (int i = 5; i <= Rows(); ++i)
{
clientdata.EquipGroupData xData = new clientdata.EquipGroupData();
xData.id = ReadInt32( i,  x++ );
xData.type = ReadInt32( i,  x++ );
xData.value = ReadInt32( i,  x++ );
xData.type_eqp = ReadInt32( i,  x++ );
xData.desc = ReadString( i,  x++ );
xData.gold = ReadInt32( i,  x++ );
xData.diamond = ReadInt32( i,  x++ );
x = 1;
xList.data.Add(xData);
}
NextSheet();
x = 1;

string fPath = System.Environment.CurrentDirectory + @"\..\..\DataClient\";
FileStream wFile = new FileStream(fPath + fileName + ".dat", FileMode.Create, FileAccess.Write);
Serializer.Serialize<EquipGroupList>(wFile, xList);
wFile.Close();

FileStream rFile = new FileStream(fPath + fileName + ".dat", FileMode.Open, FileAccess.Read);
EquipGroupList readList = Serializer.Deserialize<EquipGroupList>(rFile);
rFile.Close();
EndParse();
}

public static void ParseClientItem( string fileName )
{
StartParse(fileName);
clientdata.ItemList xList = new clientdata.ItemList();
int x = 1;
string[] sArray;
// 数据从第5行开始
for (int i = 5; i <= Rows(); ++i)
{
clientdata.ItemData xData = new clientdata.ItemData();
xData.id = ReadInt32( i,  x++ );
xData.itemname = ReadString( i,  x++ );
xData.icon = ReadString( i,  x++ );
xData.itemquality = ReadInt32( i,  x++ );
xData.itemtype = ReadInt32( i,  x++ );
xData.pile = ReadInt32( i,  x++ );
xData.desc = ReadString( i,  x++ );
xData.level = ReadInt32( i,  x++ );
xData.way_to_found = ReadString( i,  x++ );
xData.battlemodel = ReadString( i,  x++ );
xData.price = ReadInt32( i,  x++ );
xData.chestid = ReadInt32( i,  x++ );
xData.buffid = ReadInt32( i,  x++ );
xData.state_type = ReadInt32( i,  x++ );
xData.state_value = ReadInt32( i,  x++ );
x = 1;
xList.data.Add(xData);
}
NextSheet();
x = 1;

string fPath = System.Environment.CurrentDirectory + @"\..\..\DataClient\";
FileStream wFile = new FileStream(fPath + fileName + ".dat", FileMode.Create, FileAccess.Write);
Serializer.Serialize<ItemList>(wFile, xList);
wFile.Close();

FileStream rFile = new FileStream(fPath + fileName + ".dat", FileMode.Open, FileAccess.Read);
ItemList readList = Serializer.Deserialize<ItemList>(rFile);
rFile.Close();
EndParse();
}

public static void ParseClientLang( string fileName )
{
StartParse(fileName);
clientdata.LangList xList = new clientdata.LangList();
int x = 1;
string[] sArray;
// 数据从第5行开始
for (int i = 5; i <= Rows(); ++i)
{
clientdata.LangData xData = new clientdata.LangData();
xData.id = ReadInt32( i,  x++ );
xData.zh_cn = ReadString( i,  x++ );
xData.en_us = ReadString( i,  x++ );
x = 1;
xList.data.Add(xData);
}
NextSheet();
x = 1;

string fPath = System.Environment.CurrentDirectory + @"\..\..\DataClient\";
FileStream wFile = new FileStream(fPath + fileName + ".dat", FileMode.Create, FileAccess.Write);
Serializer.Serialize<LangList>(wFile, xList);
wFile.Close();

FileStream rFile = new FileStream(fPath + fileName + ".dat", FileMode.Open, FileAccess.Read);
LangList readList = Serializer.Deserialize<LangList>(rFile);
rFile.Close();
EndParse();
}

public static void ParseClientMonster( string fileName )
{
StartParse(fileName);
clientdata.MonsterList xList = new clientdata.MonsterList();
int x = 1;
string[] sArray;
// 数据从第5行开始
for (int i = 5; i <= Rows(); ++i)
{
clientdata.MonsterData xData = new clientdata.MonsterData();
xData.id = ReadInt32( i,  x++ );
xData.type = ReadInt32( i,  x++ );
xData.hp = ReadInt32( i,  x++ );
xData.hpCounter = ReadInt32( i,  x++ );
xData.speed = ReadInt32( i,  x++ );
xData.view = ReadString( i,  x++ );
xData.luascript = ReadString( i,  x++ );
xData.defaultSkill = ReadInt32( i,  x++ );
xData.appearEffect = ReadString( i,  x++ );
x = 1;
xList.data.Add(xData);
}
NextSheet();
x = 1;

string fPath = System.Environment.CurrentDirectory + @"\..\..\DataClient\";
FileStream wFile = new FileStream(fPath + fileName + ".dat", FileMode.Create, FileAccess.Write);
Serializer.Serialize<MonsterList>(wFile, xList);
wFile.Close();

FileStream rFile = new FileStream(fPath + fileName + ".dat", FileMode.Open, FileAccess.Read);
MonsterList readList = Serializer.Deserialize<MonsterList>(rFile);
rFile.Close();
EndParse();
}

public static void ParseClientMonstertable( string fileName )
{
StartParse(fileName);
clientdata.MonstertableList xList = new clientdata.MonstertableList();
int x = 1;
string[] sArray;
// 数据从第5行开始
for (int i = 5; i <= Rows(); ++i)
{
clientdata.MonstertableData xData = new clientdata.MonstertableData();
xData.id = ReadInt32( i,  x++ );
sArray = SubString(GetString(xSheet.Cells[i, x++]), ';');foreach (string sData in sArray){xData.monsterid.Add(System.Convert.ToInt32(sData));}
xData.dropid = ReadInt32( i,  x++ );
xData.rate = ReadInt32( i,  x++ );
x = 1;
xList.data.Add(xData);
}
NextSheet();
x = 1;

string fPath = System.Environment.CurrentDirectory + @"\..\..\DataClient\";
FileStream wFile = new FileStream(fPath + fileName + ".dat", FileMode.Create, FileAccess.Write);
Serializer.Serialize<MonstertableList>(wFile, xList);
wFile.Close();

FileStream rFile = new FileStream(fPath + fileName + ".dat", FileMode.Open, FileAccess.Read);
MonstertableList readList = Serializer.Deserialize<MonstertableList>(rFile);
rFile.Close();
EndParse();
}

public static void ParseClientPetBase( string fileName )
{
StartParse(fileName);
clientdata.PetBaseList xList = new clientdata.PetBaseList();
int x = 1;
string[] sArray;
// 数据从第5行开始
for (int i = 5; i <= Rows(); ++i)
{
clientdata.PetBaseData xData = new clientdata.PetBaseData();
xData.id = ReadInt32( i,  x++ );
xData.groupid = ReadInt32( i,  x++ );
xData.stars = ReadInt32( i,  x++ );
xData.baseprice = ReadInt32( i,  x++ );
xData.name = ReadString( i,  x++ );
xData.icon = ReadString( i,  x++ );
xData.model = ReadString( i,  x++ );
xData.damage = ReadInt32( i,  x++ );
xData.damage_grow = ReadInt32( i,  x++ );
xData.state_type = ReadInt32( i,  x++ );
xData.state_base = ReadInt32( i,  x++ );
xData.state_grow = ReadInt32( i,  x++ );
xData.skill_first = ReadInt32( i,  x++ );
xData.skill_first_con = ReadInt32( i,  x++ );
xData.skill_second = ReadInt32( i,  x++ );
xData.skill_second_con = ReadInt32( i,  x++ );
xData.skill_third = ReadInt32( i,  x++ );
xData.skill_third_con = ReadInt32( i,  x++ );
xData.itemid_first = ReadInt32( i,  x++ );
xData.itemid_first_num = ReadInt32( i,  x++ );
xData.itemid_second = ReadInt32( i,  x++ );
xData.itemid_second_num = ReadInt32( i,  x++ );
xData.itemid_third = ReadInt32( i,  x++ );
xData.itemid_third_num = ReadInt32( i,  x++ );
x = 1;
xList.data.Add(xData);
}
NextSheet();
x = 1;

string fPath = System.Environment.CurrentDirectory + @"\..\..\DataClient\";
FileStream wFile = new FileStream(fPath + fileName + ".dat", FileMode.Create, FileAccess.Write);
Serializer.Serialize<PetBaseList>(wFile, xList);
wFile.Close();

FileStream rFile = new FileStream(fPath + fileName + ".dat", FileMode.Open, FileAccess.Read);
PetBaseList readList = Serializer.Deserialize<PetBaseList>(rFile);
rFile.Close();
EndParse();
}

public static void ParseClientPetLevelup( string fileName )
{
StartParse(fileName);
clientdata.PetLevelupList xList = new clientdata.PetLevelupList();
int x = 1;
string[] sArray;
// 数据从第5行开始
for (int i = 5; i <= Rows(); ++i)
{
clientdata.PetLevelupData xData = new clientdata.PetLevelupData();
xData.lv = ReadInt32( i,  x++ );
xData.petstate_grew = ReadInt32( i,  x++ );
xData.petstrenth_cost = ReadInt32( i,  x++ );
x = 1;
xList.data.Add(xData);
}
NextSheet();
x = 1;

string fPath = System.Environment.CurrentDirectory + @"\..\..\DataClient\";
FileStream wFile = new FileStream(fPath + fileName + ".dat", FileMode.Create, FileAccess.Write);
Serializer.Serialize<PetLevelupList>(wFile, xList);
wFile.Close();

FileStream rFile = new FileStream(fPath + fileName + ".dat", FileMode.Open, FileAccess.Read);
PetLevelupList readList = Serializer.Deserialize<PetLevelupList>(rFile);
rFile.Close();
EndParse();
}

public static void ParseClientPetUnlock( string fileName )
{
StartParse(fileName);
clientdata.PetUnlockList xList = new clientdata.PetUnlockList();
int x = 1;
string[] sArray;
// 数据从第5行开始
for (int i = 5; i <= Rows(); ++i)
{
clientdata.PetUnlockData xData = new clientdata.PetUnlockData();
xData.groupid = ReadInt32( i,  x++ );
xData.unlockcase_first = ReadInt32( i,  x++ );
xData.unlockcase_first_value = ReadInt32( i,  x++ );
xData.unlockcase_second = ReadInt32( i,  x++ );
xData.unlockcase_second_value = ReadInt32( i,  x++ );
xData.unlockcost_type = ReadInt32( i,  x++ );
xData.unlockcost_type_value = ReadInt32( i,  x++ );
x = 1;
xList.data.Add(xData);
}
NextSheet();
x = 1;

string fPath = System.Environment.CurrentDirectory + @"\..\..\DataClient\";
FileStream wFile = new FileStream(fPath + fileName + ".dat", FileMode.Create, FileAccess.Write);
Serializer.Serialize<PetUnlockList>(wFile, xList);
wFile.Close();

FileStream rFile = new FileStream(fPath + fileName + ".dat", FileMode.Open, FileAccess.Read);
PetUnlockList readList = Serializer.Deserialize<PetUnlockList>(rFile);
rFile.Close();
EndParse();
}

public static void ParseClientSkill( string fileName )
{
StartParse(fileName);
clientdata.SkillList xList = new clientdata.SkillList();
int x = 1;
string[] sArray;
// 数据从第5行开始
for (int i = 5; i <= Rows(); ++i)
{
clientdata.SkillData xData = new clientdata.SkillData();
xData.id = ReadInt32( i,  x++ );
xData.skill_name = ReadString( i,  x++ );
xData.skill_desc = ReadString( i,  x++ );
xData.type = ReadInt32( i,  x++ );
xData.hitvalue = ReadInt32( i,  x++ );
xData.range = ReadInt32( i,  x++ );
xData.bulletid = ReadString( i,  x++ );
xData.bullettype = ReadInt32( i,  x++ );
xData.gunview = ReadString( i,  x++ );
xData.shootspeed = ReadInt32( i,  x++ );
xData.bulletspeed = ReadInt32( i,  x++ );
xData.preEffect = ReadString( i,  x++ );
xData.hitsffect = ReadString( i,  x++ );
xData.expsffect = ReadString( i,  x++ );
xData.selfbuffer1 = ReadInt32( i,  x++ );
xData.selfbuffer2 = ReadInt32( i,  x++ );
xData.hitbuffer1 = ReadInt32( i,  x++ );
xData.hitbuffer2 = ReadInt32( i,  x++ );
x = 1;
xList.data.Add(xData);
}
NextSheet();
x = 1;

string fPath = System.Environment.CurrentDirectory + @"\..\..\DataClient\";
FileStream wFile = new FileStream(fPath + fileName + ".dat", FileMode.Create, FileAccess.Write);
Serializer.Serialize<SkillList>(wFile, xList);
wFile.Close();

FileStream rFile = new FileStream(fPath + fileName + ".dat", FileMode.Open, FileAccess.Read);
SkillList readList = Serializer.Deserialize<SkillList>(rFile);
rFile.Close();
EndParse();
}

public static void ParseClientStrenth( string fileName )
{
StartParse(fileName);
clientdata.StrenthList xList = new clientdata.StrenthList();
int x = 1;
string[] sArray;
// 数据从第5行开始
for (int i = 5; i <= Rows(); ++i)
{
clientdata.StrenthData xData = new clientdata.StrenthData();
xData.strenthlevel = ReadInt32( i,  x++ );
xData.damageup = ReadInt32( i,  x++ );
xData.hpup = ReadInt32( i,  x++ );
xData.armorup = ReadInt32( i,  x++ );
xData.strenthcost = ReadInt32( i,  x++ );
x = 1;
xList.data.Add(xData);
}
NextSheet();
x = 1;

string fPath = System.Environment.CurrentDirectory + @"\..\..\DataClient\";
FileStream wFile = new FileStream(fPath + fileName + ".dat", FileMode.Create, FileAccess.Write);
Serializer.Serialize<StrenthList>(wFile, xList);
wFile.Close();

FileStream rFile = new FileStream(fPath + fileName + ".dat", FileMode.Open, FileAccess.Read);
StrenthList readList = Serializer.Deserialize<StrenthList>(rFile);
rFile.Close();
EndParse();
}

public static void ParseClientZone( string fileName )
{
StartParse(fileName);
clientdata.ZoneList xList = new clientdata.ZoneList();
int x = 1;
string[] sArray;
// 数据从第5行开始
for (int i = 5; i <= Rows(); ++i)
{
clientdata.ZoneData xData = new clientdata.ZoneData();
xData.id = ReadInt32( i,  x++ );
xData.chapter = ReadInt32( i,  x++ );
xData.stage = ReadInt32( i,  x++ );
xData.difficulty = ReadInt32( i,  x++ );
xData.prezone = ReadInt32( i,  x++ );
xData.exp = ReadInt32( i,  x++ );
xData.gold = ReadInt32( i,  x++ );
xData.type = ReadInt32( i,  x++ );
xData.dropid = ReadInt32( i,  x++ );
xData.time = ReadInt32( i,  x++ );
xData.mapview = ReadString( i,  x++ );
xData.openview = ReadString( i,  x++ );
sArray = SubString(GetString(xSheet.Cells[i, x++]), ';');foreach (string sData in sArray){xData.monstertable.Add(System.Convert.ToInt32(sData));}
xData.desc = ReadString( i,  x++ );
x = 1;
xList.data.Add(xData);
}
NextSheet();
x = 1;

string fPath = System.Environment.CurrentDirectory + @"\..\..\DataClient\";
FileStream wFile = new FileStream(fPath + fileName + ".dat", FileMode.Create, FileAccess.Write);
Serializer.Serialize<ZoneList>(wFile, xList);
wFile.Close();

FileStream rFile = new FileStream(fPath + fileName + ".dat", FileMode.Open, FileAccess.Read);
ZoneList readList = Serializer.Deserialize<ZoneList>(rFile);
rFile.Close();
EndParse();
}

public static void ParseClient(string fileName)
{
if ( fileName.Contains( "$" ) )
{ return; }
switch (fileName)
{
case "Buffer":
{ParseClientBuffer( fileName ); }
break;
case "Character":
{ParseClientCharacter( fileName ); }
break;
case "Compose":
{ParseClientCompose( fileName ); }
break;
case "Drop":
{ParseClientDrop( fileName ); }
break;
case "Equip":
{ParseClientEquip( fileName ); }
break;
case "EquipGroup":
{ParseClientEquipGroup( fileName ); }
break;
case "Item":
{ParseClientItem( fileName ); }
break;
case "Lang":
{ParseClientLang( fileName ); }
break;
case "Monster":
{ParseClientMonster( fileName ); }
break;
case "Monstertable":
{ParseClientMonstertable( fileName ); }
break;
case "PetBase":
{ParseClientPetBase( fileName ); }
break;
case "PetLevelup":
{ParseClientPetLevelup( fileName ); }
break;
case "PetUnlock":
{ParseClientPetUnlock( fileName ); }
break;
case "Skill":
{ParseClientSkill( fileName ); }
break;
case "Strenth":
{ParseClientStrenth( fileName ); }
break;
case "Zone":
{ParseClientZone( fileName ); }
break;
default:
break;
}
}
}
}
