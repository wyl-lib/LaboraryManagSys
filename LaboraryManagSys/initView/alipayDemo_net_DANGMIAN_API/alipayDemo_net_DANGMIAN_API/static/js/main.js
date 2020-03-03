//控制form表单
var Form = function(data,body,url){
  this.tplt = '<form class="form-horizontal">'
            +'<div class="js-form-body">'
            +'</div>'
            +'<div class="form-group">'
            +'  <label class="col-md-2 control-label">执行结果：</label>'
            +'  <div class="col-md-10">'
            +'    <textarea class="form-control js-result" rows="3"></textarea>'
            +'  </div>'
            +'</div>'
            +' <div class="form-group">'
            +'  <div class="col-md-offset-10 col-md-2" style="text-align: right;">'
            +'    <button style="width: 120px;" class="btn btn-default btn-blue js-btn-submit">提交</button>'
            +'  </div>'
            +'</div>'
          +'</form>';
    this.component = [];
    this.data = data;
    this.url = url;
    this.body = body;
    this.$form = $(this.tplt);
    this.init()
}

Form.prototype.init = function(){
  var $form = this.$form ;
  var self = this;
  $form.find('.js-form-body').append(this.body).end()
    .find('.js-btn-submit').on('click',function(){
      self.submit()
            return false;
    }).end()
  this.component = [$form];
}
Form.prototype.setResult = function(val){
  this.$form.find('.js-result').val(val);
}
Form.prototype.getComponent = function(){
  return this.component;
}
Form.prototype.setFormBody = function(component){
    this.$form.find('.js-form-body').append(component);
}
Form.prototype.submit = function(){
    var data = this.$form.serializeArray();
    var params = {};
    var self = this;

    for(var i in data){
        data[i].value&&(params[data[i].name] = data[i].value);//过滤空字符串
    }

    $ .ajax({
        url:self.url,
        data:params,
        type:"post",
        success:function(result){
            self.setResult(JSON.stringify(result));
        }
    })
}


//================================================

var TreePanel = function(data){
  // this.$el = $(el);
  this.data = data;
  this.panelTplt = '<div class="panel panel-default">'
             +' <div class="panel-heading">'
             +'   <span class="panel-title"></span>'
             +'   <span class="panel-collaspe">收起</span>'
             +' </div>'
             +' <div class="panel-body">'
             +' </div>'
            +'</div>';
  this.formTplt = '<div class="form-group">'
                  +'<div class="row"><label class="col-md-2 control-label"></label>'
                  +'<div class="col-md-5">'
                  +'  <input type="text" name="" class="form-control"  placeholder="请输入……">'
                  +'</div></div>'
                  +'<div class="row"><div class="col-md-offset-2 col-md-5">'
                  +'  <p class="notice"></p>'
                  +'</div></div>'
                +'</div>'
    this.listTplt = '<label class="col-md-2 control-label"></label>'
                  +'<div class="col-md-2">'
                  +'  <input type="text" name="" class="form-control"  placeholder="请输入……">'
                  +'</div>'
    this.component = [];
    this.init()
}

TreePanel.prototype.init = function(){
    if(!this.data) return;
    var self = this;
    //渲染子集panel
    function compleTplt(data,required){
        if (!data) {return};
        data = data instanceof Array?data:[data]
        var result = [];                

        for(var k in data){
            if(data[k].childs.length>0){
                var $childPanel = $(self.panelTplt)
                $childPanel.find('.panel-body').append(data[k].isListType?compleListTplt(data[k].childs,required):compleTplt(data[k].childs,required)).end()
                    .find('.panel-title').html(data[k].title||'').end()
                result.push($childPanel);
            }else{
                var label = required&&data[k].isMust=='1'?'<span class="red">*</span>'+data[k].title:data[k].title
                var $form = $(self.formTplt);
                $form.find('label').html(label).end()
                  .find('input').attr('name',data[k].fullParamName).end()
                  .find('.notice').html(data[k].desc).end();
                result.push($form);
            }
        }
        return result
    }
    //渲染list
    function compleListTplt(data,required){
        if (!(data instanceof Array)||data.length<1) {return};
        var result = [],list = [];

        
        for(var i in data){
            //对fullParamName进行特殊处理，原数据格式goodsDetail[0].goodsCategory
            // var fullParamName = data[i].fullParamName.replace(/\[.*\]/,'['+i+']')
            var label = required&&data[i].isMust=='1'?'<span class="red">*</span>'+data[i].title:data[i].title
            var listTplt = '<label class="col-md-2 control-label">'+label+'</label>'
                  +'<div class="col-md-2">'
                  +'  <input type="text" name="'+data[i].fullParamName+'" class="form-control"  placeholder="请输入……">'
                  +'</div>'
            if (i%3==2) {//每行显示3个
                list.push(listTplt);
                var wrapListTplt = '<div class="form-group">'+list.join('')+'</div>'
                result.push(wrapListTplt);
                list = [];
            }else{
                list.push(listTplt);
            }
        }
        //清空list
        if (list.length>0) {
            var wrapListTplt = '<div class="form-group">'+list.join('')+'</div>'
            result.push(wrapListTplt);
        }

        function delList(el){
            var li = $(el).parents('.list-group').eq(0);
            var list = $(el).parents('.panel-body').eq(0).find('.list-group');
            var index = list.index(li);
            var isModifyName = index!=(list.length-1)
            li.remove();
            list.splice(index,1)
            
            if(isModifyName){
                //此时需要遍历修改name
                for(var i=0;i<list.length;i++){
                    $(list[i]).find('input').each(function(){
                        var name = $(this).attr('name').replace(/\[.*\]/,'['+i+']');
                        $(this).attr('name',name);
                    })
                }
            }

            
        }

        result = '<div class="list-group"><div class="list-header"><span class="glyphicon glyphicon-trash js-del"></span></div>'+result.join('')+'</div>'

        //还需要增加增加和删除功能
        var i = 1;
        var addBtn = '<div class="form-group">'
                     +'   <div class="col-md-2" style="text-align: right;">'
                     +'     <button class="btn btn-default btn-blue"><span class="glyphicon glyphicon-plus"></span>&nbsp;添加</button>'
                     +'   </div>'
                     +' </div>';
        var $addBtn = $(addBtn).find('button').on('click',function(){
            var $tplt = $(result).find('.js-del').on('click',function(){
                delList(this);
            }).end()
            $tplt.find('input').each(function(){
                var fullParamName = $(this).attr('name')
                $(this).attr('name',fullParamName.replace(/\[.*\]/,'['+i+']'))
            })
            $(this).parents('.form-group').eq(0).before($tplt);
            i++;
            return false;
        }).end();

        //删除功能，在删除的时候，需要重新遍历list，修改name
        var $result = $(result).find('.js-del').on('click',function(){
            delList(this)
        }).end()
        return [$result,$addBtn]
    }
    
    //渲染一级panel
    var requiredAry = [],optionAry = [];
    for(var k in this.data){
        if (this.data[k].isMust=='1') {
            requiredAry = requiredAry.concat(compleTplt(this.data[k],true))
        }else{
            optionAry = optionAry.concat(compleTplt(this.data[k],false))
        }
    }
  
    var $requiredPanel = $(this.panelTplt);
    var $optionPanel = $(this.panelTplt); 
    requiredAry.length>0&&$requiredPanel.find('.panel-body').append(requiredAry).end()
        .find('.panel-title').eq(0).html('必填项').end().end()
        .on('click','.panel-collaspe',function() {
            var $panelBody = $(this).parents('.panel').eq(0).find('.panel-body').eq(0);
            $panelBody.hasClass('hide')
                ?$panelBody.removeClass('hide')&&$(this).html('收起')
                :$panelBody.addClass('hide')&&$(this).html('展开')
          });

    optionAry.length>0&&$optionPanel.find('.panel-body').append(optionAry).end()
    .find('.panel-title').eq(0).html('可选项').end().end()
    .on('click','.panel-collaspe',function() {
        var $panelBody = $(this).parents('.panel').eq(0).find('.panel-body').eq(0);
        $panelBody.hasClass('hide')
                ?$panelBody.removeClass('hide')&&$(this).html('收起')
                :$panelBody.addClass('hide')&&$(this).html('展开')
      });
    this.component = this.component.concat([$requiredPanel,$optionPanel])
    // this.$el.append([$requiredPanel,$optionPanel])
}  

TreePanel.prototype.getComponent = function(){
    return this.component;
}

//============================================
//var data = [{apiZhName:'haha',apiNameFirstLower:'hha'},{"apiName":"alipay.trade.precreate","apiZhName":"统一收单线下交易预创建","invokeType":1,"apiInParam":[{"childs":[],"isListType":false,"isMust":1,"baseType":"BASETYPE","description":null,"fullParamName":"outTradeNo","title":"商户订单号","desc":"64个字符以内、只能包含字母、数字、下划线；需保证在商户端不重复","listType":false},{"childs":[],"isListType":false,"isMust":1,"baseType":"BASETYPE","description":null,"fullParamName":"totalAmount","title":"订单总金额","desc":"单位为元，精确到小数点后两位，取值范围[0.01,100000000] 如果同时传入了【打折金额】，【不可打折金额】，【订单总金额】三者，则必须满足如下条件：【订单总金额】=【打折金额】+【不可打折金额】","listType":false},{"childs":[],"isListType":false,"isMust":1,"baseType":"BASETYPE","description":null,"fullParamName":"subject","title":"订单标题","desc":"","listType":false},{"childs":[],"isListType":false,"isMust":3,"baseType":"BASETYPE","description":null,"fullParamName":"sellerId","title":"卖家支付宝用户ID","desc":" 如果该值为空，则默认为商户签约账号对应的支付宝用户ID","listType":false},{"childs":[],"isListType":false,"isMust":3,"baseType":"BASETYPE","description":null,"fullParamName":"discountableAmount","title":"可打折金额","desc":" 参与优惠计算的金额，单位为元，精确到小数点后两位，取值范围[0.01,100000000] 如果该值未传入，但传入了【订单总金额】，【不可打折金额】则该值默认为【订单总金额】-【不可打折金额】","listType":false},{"childs":[],"isListType":false,"isMust":3,"baseType":"BASETYPE","description":null,"fullParamName":"undiscountableAmount","title":"不可打折金额","desc":" 不参与优惠计算的金额，单位为元，精确到小数点后两位，取值范围[0.01,100000000] 如果该值未传入，但传入了【订单总金额】,【打折金额】，则该值默认为【订单总金额】-【打折金额】","listType":false},{"childs":[],"isListType":false,"isMust":3,"baseType":"BASETYPE","description":null,"fullParamName":"buyerLogonId","title":"买家支付宝账号","desc":"","listType":false},{"childs":[{"childs":[],"isListType":false,"isMust":1,"baseType":"BASETYPE","description":null,"fullParamName":"goodsDetail[0].goodsId","title":"商品的编号","desc":"","listType":false},{"childs":[],"isListType":false,"isMust":1,"baseType":"BASETYPE","description":null,"fullParamName":"goodsDetail[0].goodsName","title":"商品名称","desc":"","listType":false},{"childs":[],"isListType":false,"isMust":1,"baseType":"BASETYPE","description":null,"fullParamName":"goodsDetail[0].quantity","title":"商品数量","desc":"","listType":false},{"childs":[],"isListType":false,"isMust":1,"baseType":"BASETYPE","description":null,"fullParamName":"goodsDetail[0].price","title":"商品单价","desc":"单位为元","listType":false},{"childs":[],"isListType":false,"isMust":3,"baseType":"BASETYPE","description":null,"fullParamName":"goodsDetail[0].alipayGoodsId","title":"支付宝定义的统一商品编号","desc":"","listType":false},{"childs":[],"isListType":false,"isMust":3,"baseType":"BASETYPE","description":null,"fullParamName":"goodsDetail[0].goodsCategory","title":"商品类目","desc":"","listType":false},{"childs":[],"isListType":false,"isMust":3,"baseType":"BASETYPE","description":null,"fullParamName":"goodsDetail[0].body","title":"商品描述信息","desc":"","listType":false},{"childs":[],"isListType":false,"isMust":3,"baseType":"BASETYPE","description":null,"fullParamName":"goodsDetail[0].showUrl","title":"商品的展示地址","desc":"","listType":false}],"isListType":true,"isMust":3,"baseType":"COMPLEXTYPE","description":null,"fullParamName":"goodsDetail[0]","title":"订单包含的商品列表信息","desc":"Json格式. 其它说明详见：“商品明细说明”","listType":true},{"childs":[],"isListType":false,"isMust":3,"baseType":"BASETYPE","description":null,"fullParamName":"body","title":"对交易或商品的描述","desc":"","listType":false},{"childs":[],"isListType":false,"isMust":3,"baseType":"BASETYPE","description":null,"fullParamName":"operatorId","title":"商户操作员编号","desc":"","listType":false},{"childs":[],"isListType":false,"isMust":3,"baseType":"BASETYPE","description":null,"fullParamName":"storeId","title":"商户门店编号","desc":"","listType":false},{"childs":[],"isListType":false,"isMust":3,"baseType":"BASETYPE","description":null,"fullParamName":"disablePayChannels","title":"禁用渠道","desc":"用户不可用指定渠道支付  当有多个渠道时用“,”分隔  注，与enable_pay_channels互斥","listType":false},{"childs":[],"isListType":false,"isMust":3,"baseType":"BASETYPE","description":null,"fullParamName":"enablePayChannels","title":"可用渠道","desc":"用户只能在指定渠道范围内支付  当有多个渠道时用“,”分隔  注，与disable_pay_channels互斥","listType":false},{"childs":[],"isListType":false,"isMust":3,"baseType":"BASETYPE","description":null,"fullParamName":"terminalId","title":"商户机具终端编号","desc":"","listType":false},{"childs":[{"childs":[],"isListType":false,"isMust":3,"baseType":"BASETYPE","description":null,"fullParamName":"extendParams.sysServiceProviderId","title":"系统商编号","desc":" 该参数作为系统商返佣数据提取的依据，请填写系统商签约协议的PID","listType":false},{"childs":[],"isListType":false,"isMust":3,"baseType":"BASETYPE","description":null,"fullParamName":"extendParams.hbFqNum","title":"使用花呗分期要进行的分期数","desc":"","listType":false},{"childs":[],"isListType":false,"isMust":3,"baseType":"BASETYPE","description":null,"fullParamName":"extendParams.hbFqSellerPercent","title":"使用花呗分期需要卖家承担的手续费比例的百分值","desc":"传入100代表100%","listType":false}],"isListType":false,"isMust":3,"baseType":"COMPLEXTYPE","description":null,"fullParamName":"extendParams","title":"业务扩展参数","desc":"","listType":false},{"childs":[],"isListType":false,"isMust":3,"baseType":"BASETYPE","description":null,"fullParamName":"timeoutExpress","title":"该笔订单允许的最晚付款时间","desc":"逾期将关闭交易。取值范围：1m～15d。m-分钟，h-小时，d-天，1c-当天（1c-当天的情况下，无论交易何时创建，都在0点关闭）。 该参数数值不接受小数点， 如 1.5h，可转换为 90m。","listType":false},{"childs":[{"childs":[{"childs":[],"isListType":false,"isMust":1,"baseType":"BASETYPE","description":null,"fullParamName":"royaltyInfo.royaltyDetailInfos[0].batchNo","title":"分账批次号","desc":" 分账批次号。  目前需要和转入账号类型为bankIndex配合使用。","listType":false},{"childs":[],"isListType":false,"isMust":1,"baseType":"BASETYPE","description":null,"fullParamName":"royaltyInfo.royaltyDetailInfos[0].transOutType","title":"要分账的账户类型","desc":"  目前只支持userId：支付宝账号对应的支付宝唯一用户号。  默认值为userId。","listType":false},{"childs":[],"isListType":false,"isMust":1,"baseType":"BASETYPE","description":null,"fullParamName":"royaltyInfo.royaltyDetailInfos[0].transOut","title":"如果转出账号类型为userId","desc":"本参数为要分账的支付宝账号对应的支付宝唯一用户号。以2088开头的纯16位数字。","listType":false},{"childs":[],"isListType":false,"isMust":1,"baseType":"BASETYPE","description":null,"fullParamName":"royaltyInfo.royaltyDetailInfos[0].transIn","title":"如果转入账号类型为userId","desc":"本参数为接受分账金额的支付宝账号对应的支付宝唯一用户号。以2088开头的纯16位数字。  &#61548;\t如果转入账号类型为bankIndex，本参数为28位的银行编号（商户和支付宝签约时确定）。  如果转入账号类型为storeId，本参数为商户的门店ID。","listType":false},{"childs":[],"isListType":false,"isMust":1,"baseType":"BASETYPE","description":null,"fullParamName":"royaltyInfo.royaltyDetailInfos[0].amount","title":"分账的金额","desc":"单位为元","listType":false},{"childs":[],"isListType":false,"isMust":3,"baseType":"BASETYPE","description":null,"fullParamName":"royaltyInfo.royaltyDetailInfos[0].serialNo","title":"分账序列号","desc":"表示分账执行的顺序，必须为正整数","listType":false},{"childs":[],"isListType":false,"isMust":3,"baseType":"BASETYPE","description":null,"fullParamName":"royaltyInfo.royaltyDetailInfos[0].transInType","title":"接受分账金额的账户类型：","desc":" &#61548;\tuserId：支付宝账号对应的支付宝唯一用户号。  &#61548;\tbankIndex：分账到银行账户的银行编号。目前暂时只支持分账到一个银行编号。  storeId：分账到门店对应的银行卡编号。  默认值为userId。","listType":false},{"childs":[],"isListType":false,"isMust":3,"baseType":"BASETYPE","description":null,"fullParamName":"royaltyInfo.royaltyDetailInfos[0].outRelationId","title":"商户分账的外部关联号","desc":"用于关联到每一笔分账信息，商户需保证其唯一性。  如果为空，该值则默认为“商户网站唯一订单号+分账序列号”","listType":false},{"childs":[],"isListType":false,"isMust":3,"baseType":"BASETYPE","description":null,"fullParamName":"royaltyInfo.royaltyDetailInfos[0].desc","title":"分账描述信息","desc":"","listType":false},{"childs":[],"isListType":false,"isMust":3,"baseType":"BASETYPE","description":null,"fullParamName":"royaltyInfo.royaltyDetailInfos[0].amountPercentage","title":"分账的比例","desc":"值为20代表按20%的比例分账","listType":false}],"isListType":true,"isMust":1,"baseType":"COMPLEXTYPE","description":null,"fullParamName":"royaltyInfo.royaltyDetailInfos[0]","title":"分账明细的信息","desc":"可以描述多条分账指令，json数组。","listType":true},{"childs":[],"isListType":false,"isMust":3,"baseType":"BASETYPE","description":null,"fullParamName":"royaltyInfo.royaltyType","title":"分账类型","desc":" 卖家的分账类型，目前只支持传入ROYALTY（普通分账类型）。","listType":false}],"isListType":false,"isMust":3,"baseType":"COMPLEXTYPE","description":null,"fullParamName":"royaltyInfo","title":"描述分账信息","desc":"json格式。","listType":false},{"childs":[{"childs":[],"isListType":false,"isMust":1,"baseType":"BASETYPE","description":null,"fullParamName":"subMerchant.merchantId","title":"间连受理商户的支付宝商户编号","desc":"通过间连商户入驻后得到。间连业务下必传，并且需要按规范传递受理商户编号。","listType":false}],"isListType":false,"isMust":3,"baseType":"COMPLEXTYPE","description":null,"fullParamName":"subMerchant","title":"二级商户信息","desc":"当前只对特殊银行机构特定场景下使用此字段","listType":false},{"childs":[],"isListType":false,"isMust":3,"baseType":"BASETYPE","description":null,"fullParamName":"alipayStoreId","title":"支付宝店铺的门店ID","desc":"","listType":false},{"childs":[{"childs":[],"isListType":false,"isMust":3,"baseType":"BASETYPE","description":null,"fullParamName":"extUserInfo.name","title":"姓名","desc":"   注： need_check_info=T时该参数才有效","listType":false},{"childs":[],"isListType":false,"isMust":3,"baseType":"BASETYPE","description":null,"fullParamName":"extUserInfo.mobile","title":"手机号","desc":" 注：该参数暂不校验","listType":false},{"childs":[],"isListType":false,"isMust":3,"baseType":"BASETYPE","description":null,"fullParamName":"extUserInfo.certType","title":"身份证：IDENTITY_CARD、护照：PASSPORT、军官证：OFFICER_CARD、士兵证：SOLDIER_CARD、户口本：HOKOU等","desc":"如有其它类型需要支持，请与蚂蚁金服工作人员联系。    注： need_check_info=T时该参数才有效","listType":false},{"childs":[],"isListType":false,"isMust":3,"baseType":"BASETYPE","description":null,"fullParamName":"extUserInfo.certNo","title":"证件号","desc":"   注：need_check_info=T时该参数才有效","listType":false},{"childs":[],"isListType":false,"isMust":3,"baseType":"BASETYPE","description":null,"fullParamName":"extUserInfo.minAge","title":"允许的最小买家年龄","desc":"买家年龄必须大于等于所传数值   注：  1. need_check_info=T时该参数才有效  2. min_age为整数，必须大于等于0","listType":false},{"childs":[],"isListType":false,"isMust":3,"baseType":"BASETYPE","description":null,"fullParamName":"extUserInfo.fixBuyer","title":"是否强制校验付款人身份信息","desc":" T:强制校验，F：不强制","listType":false},{"childs":[],"isListType":false,"isMust":3,"baseType":"BASETYPE","description":null,"fullParamName":"extUserInfo.needCheckInfo","title":"是否强制校验身份信息","desc":" T:强制校验，F：不强制","listType":false}],"isListType":false,"isMust":3,"baseType":"COMPLEXTYPE","description":null,"fullParamName":"extUserInfo","title":"外部指定买家","desc":"","listType":false},{"childs":[],"isListType":false,"isMust":3,"baseType":"BASETYPE","description":null,"fullParamName":"businessParams","title":"商户传入业务信息","desc":"具体值要和支付宝约定，应用于安全，营销等参数直传场景，格式为json格式","listType":false}],"apiOutParam":[{"childs":[],"isListType":false,"isMust":1,"baseType":"BASETYPE","description":null,"fullParamName":"outTradeNo","title":"商户的订单号","desc":"","listType":false},{"childs":[],"isListType":false,"isMust":1,"baseType":"BASETYPE","description":null,"fullParamName":"qrCode","title":"当前预下单请求生成的二维码码串","desc":"可以用二维码生成工具根据该码串值生成对应的二维码","listType":false}],"apiNameFirstUpper":"AlipayTradePrecreate","apiNameFirstLower":"alipayTradePrecreate"}]
