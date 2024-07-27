1.首先要在实体上添加特性验证信息
	 public class OrderRequest
     {
          /// <summary>
          /// 订单号
          /// </summary>
          [Validate(ValidateType.IsEmpty)]
          public string OrderNo { get; set; }

          /// <summary>
          /// 商品名称
          /// </summary>
          [Validate(ValidateType.IsEmpty|ValidateType.MaxLength,MaxLength = 50)]
          public string CommodityName { get; set; }

          /// <summary>
          /// 商品数量
          /// </summary>
          [Validate(ValidateType.IsEmpty|ValidateType.IsNumber)]
          public string CommodityAmount { get; set; }

          /// <summary>
          /// 商品重量
          /// </summary>
          [Validate(ValidateType.IsEmpty | ValidateType.IsDecimal)]
          public string CommodityWeight { get; set; }

          /// <summary>
          /// 商品价格
          /// </summary>
          [Validate(ValidateType.IsEmpty | ValidateType.IsDecimal)]
          public string CommodityValue { get; set; }

          /// <summary>
          /// 希望到货时间
          /// </summary>
          [Validate(ValidateType.IsEmpty | ValidateType.IsDateTime)]
          public string HopeArriveTime { get; set; }

          /// <summary>
          /// 结算方式
          /// </summary>
          [Validate(ValidateType.IsEmpty | ValidateType.IsInCustomArray,CustomArray = new string[]{"现结","到付","月结"})]
          public string PayMent { get; set;}

          /// <summary>
          /// 备注
          /// </summary>
          [Validate(ValidateType.MaxLength,MaxLength = 256)]
          public string Remark { get; set; }
     } 

2.最后，我们只需调用这么一句代码，就可以实现对整个实体类的元素的验证：

	//验证订单
	string checkMessage = EntityValidation.GetValidateResult(orderRequest);

	if(!string.IsNullOrEmpty(checkMessage))
	   return checkMessage;

	//do something....

3.另外可以考虑使用微软企业库里面的Validation Application Block (VAB). 