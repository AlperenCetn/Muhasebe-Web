$("#stokveri").on("submit",
    function (event) {
        event.preventDefault();


        var dg = {
            sf: $("#SatishFiyat").val(),
            af: $("#AlishFiyat").val(),
            F2: $("#F2").val(),
            F3: $("#F3").val(),
            F4: $("#F4").val(),
            F5: $("#F5").val(),
            F6: $("#F6").val(),
            F7: $("#F7").val(),
            F8: $("#F8").val(),
            F9: $("#F9").val(),
            F10: $("#F10").val(),

        };
        var ktg = {
            Ktg1: $("#Ktg1").val(),
            Ktg2: $("#Ktg2").val(),
            Ktg3: $("#Ktg3").val(),
            Ktg4: $("#Ktg4").val(),
            Ktg5: $("#Ktg5").val(),
            Ktg6: $("#Ktg6").val(),
        };


        var obj = {
            id: $("#sID").val(),
            StokKodu: $("#StokK").val(),
            UrunAdi: $("#UrunA").val(),
            Barkod: $("#YeniBarkod").val(),
            StoktaKalan: $("#StoktaKalan").val(),


            AlishFiyat: $("#AlishFiyat").val(),
            //buraya kar oranının kayıt edileceği yer gelecek
            SatishFiyat: $("#SatishFiyat").val(),
            Marka: $("#Marka").val(),
            Grubu: $("#Grubu").val(),
            KDV: $("#KDV").val(),
            Birimi: $("#Birimi").val(),
            ParaBirimi: $("#ParaBirimi").val(),
            UrunTuru: $("#UrunTuru").val(),
            KalanStkU: $("#KalanStkU").val(),
            //KisaKod: $("#KisaKod").val(),
            //PartiNo: $("#PartiNo").val(),
            //Detay2: $("#Detay2").val(),
            //OEM: $("#OEM").val(),


            //P2: $("#P2").val(),
            //P3: $("#P3").val(),
            //P4: $("#P4").val(),
            //P5: $("#P5").val(),
            //P6: $("#P6").val(),

            //BRM2: $("#BRM2").val(),
            //BRM3: $("#BRM3").val(),
            //BRM4: $("#BRM4").val(),
            //BRM5: $("#BRM5").val(),
            //BRM6: $("#BRM6").val(),

            //nbadet2: $("#nbadet2").val(),
            //nbadet3: $("#nbadet3").val(),
            //nbadet4: $("#nbadet4").val(),
            //nbadet5: $("#nbadet5").val(),
            //nbadet6: $("#nbadet6").val(),

            Brkd2: $("#Brk2").val(),
            Brkd3: $("#Brk3").val(),
            Brkd4: $("#Brk4").val(),
            Brkd5: $("#Brk5").val(),
            Brkd6: $("#Brk6").val(),
            SdepoID: $("#DepoID").val(),
            //Marka: $("#Marka").val(),
            //Grubu: $("#Grubu").val(),
            //KDV: $("#KDV").val(),
            //Birimi: $("#Birimi").val(),
            //ParaBirimi: $("#ParaBirimi").val(),
            //UrunTuru: $("#UrunTuru").val(),
            //KisaKod: $("#KisaKod").val(),
            //PartiNo: $("#PartiNo").val(),
            //Detay2: $("#Detay2").val(),
            //OEM: $("#OEM").val(),
        };

        $.ajax({
            type: "POST",
            url: '/Stok/YeniStok',
            data: { st: obj, dg: dg, ktg: ktg },
            dataType: "json",
            success: function (gelenDeg) {

                if (gelenDeg.sonuc == "0") {
                    swal("Uyarı", gelenDeg.Message, "warning");
                    $('#stokid').modal('hide');
                }
                else if (gelenDeg.sonuc == "3") {
                    swal("Hata", gelenDeg.Message, "error");
                    $('#stokid').modal('hide');
                }
                else if (gelenDeg.sonuc == "1") {
                    swal("Başarılı", gelenDeg.Message, "success");
                    {
                        $('#stokid').modal('hide');
                        location.reload(true);
                        setTimeout(oTable.ajax.reload(), 300);
                    }

                }

            },
            error: function () {
                swal("Hata", "Kayıt Eklenirken Hata Oluştu!", "error");
            }
        });
    });
