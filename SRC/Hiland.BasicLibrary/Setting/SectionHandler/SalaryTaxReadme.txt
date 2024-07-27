0、算法理论与依据
	2011最新个税税率表 从今年9月1日起，修改后的个税法将正式实施，个税起征点将从现行的2000元提高到3500元，税率由九级改为七级，为3%至45%。(税率如下所示)  个人所得税税率表（一）  
	级数 		全月应纳税所得额 			税率		速算扣除数 
	1  不超过1500元的部分 				3% 		0 
	2 超过1500元至4500元的部分		10% 		105
	3 超过4500元至9000元的部分 		20% 		555 
	4 超过9000元至35000元的部分 		25% 		1005 
	5 超过35000元至55000元的部分 	30% 		2755 
	6 超过55000元至80000元的部分 	35%		5505 
	7 超过80000元的部分 					45%		13505  
	月薪应纳税额 
	全月应纳税所得额=月薪金收入总额（包括加班费等）-3500 -个人支付的社保和公积金费用 - 各种迟到早退等罚款
	全月应纳税额=全月应纳税所得额×适用税率－速算扣除

1、config配置
	1.1、
		<section name="SalaryTaxSection" type="Hiland.BasicLibrary.Setting.SectionHandler.SalaryTaxSectionHandler,Hiland.BasicLibrary"/>
	1.2、
		<SalaryTaxSection taxThreshold="3500">
			<add name="Level0" min="-9999999999" max="0" rate="0" expressCalcValue="0" />
			<add name="Level1" min="0" max="1500" rate="0.03" expressCalcValue="0" />
			<add name="Level2" min="1500" max="4500" rate="0.1" expressCalcValue="105" />
			<add name="Level3" min="4500" max="9000" rate="0.2" expressCalcValue="555" />
			<add name="Level4" min="9000" max="35000" rate="0.25" expressCalcValue="1005" />
			<add name="Level5" min="35000" max="55000" rate="0.3" expressCalcValue="2755" />
			<add name="Level6" min="55000" max="80000" rate="0.35" expressCalcValue="5505" />
			<add name="Level7" min="80000" max="9999999999" rate="0.45" expressCalcValue="13505" />
		</SalaryTaxSection>

2、调用，使用SalaryTaxHelper进行计算应缴税（Hiland.BasicLibrary\Finance\SalaryTaxHelper.cs）
	SalaryTaxHelper.GetSalaryTax(inputData);
	

