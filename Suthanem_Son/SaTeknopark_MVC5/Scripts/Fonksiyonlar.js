function FiyatGetir(id, brk) {

    $.ajax({
        type: "GET",
        "datatype": "json",
        url: '/Tables/StokBilgi?id=' + id+'&Brk='+brk,
        success: function (gt) {
            if (gt.success) {

                if (gt.kodbulundu) {
                    $("#Barkod").val(gt.data.Barkod);
                } else {

                    $("#StokKodu").select2("val", gt.data.ID);
                 
                }
                 
                $("#Birim").val(gt.data.Birimi);
                $("#BirimFiyati").val(Number(gt.data.SatishFiyat).toFixed(2).replace(".", ","));
                $("#KDV").val(gt.data.KDV);
                Topla();

                
            }
        }

    });


}

function RaporGoster(id,type) {

    
     var win = null;


     if (type == "SI" || type == "YI") {

         win = window.open("/Adv/IrsaliyeYazdir?ID=" + id);

     }
     
     else if (type == "DI") {

         win = window.open("/Adv/DepoIslemYazdir?ID=" + id);

     }
     else if (type == "T" || type == "G" || type == "KKT") {

         win = window.open("/Adv/TahsilatYazdir?ID=" + id);

     }
    win.focus();

}