$("#cariveri").on("submit", function (event) {
            event.preventDefault();

            var obj = {
                id: $("#cID").val(),
                FirmaKodu: $("#FirmaK").val(),
                CariUnvan: $("#CariU").val(),
                Il: $("#Il").val(),
                EkBilgi: $("#EkBilgi").val(),
                Adres: $("#Adres").val(),
                EPosta: $("#EPos").val(),
                Gsm: $("#Gsm").val(),
                Telefon1: $("#Telefon1").val(),
                KTipi: $("#KTip").val(),
                StokFG: $("#StokFG").val(),
                CariGrubu: $("#CariGrubu").val(),
                VergiDr: $("#VergiDr").val(),
                VergiNo: $("#VergiNo").val(),
                PostaKodu: $("#PostaKodu").val(),
                Yetkili: $("#Yetkili").val(),
                Milleti: $("#Ulke").val(),
                Faks: $("#Faks").val(),
                Yetkili: $("#Yetkili").val(),
                ozelAlan1: $("#ozelAlan1").val(),
                ozelAlan2: $("#ozelAlan2").val(),
                ozelAlan3: $("#ozelAlan3").val(),
                ozelAlan4: $("#ozelAlan4").val(),
                ozelAlan5: $("#ozelAlan5").val(),
                TCNo: $("#TCNo").val(),
                ParaBirimi: $("#ParaBirimi").val(),
                Iskonto: $("#Iskonto").val(),
                MersisNo: $("#MersisNo").val(),
                TicaretSicilNo: $("#TicaretSicilNo").val(),
                BagkurNo: $("#BagkurNo").val(),
                WebKA: $("#WebKA").val(),
                WebSifre: $("#WebSifre").val(),
                Ilce: $("#Ilce").val(),
                WebSite: $("#WebSite").val(),
                MusteriTemsilcisi: $("#MusteriTemsilcisi").val(),
            };
            var alacakB = $("#alacakB").val();
            var borcB = $("#borcB").val();

            $.ajax({
                type: "POST",
                url: '/Tables/YeniCari',
                data: { cr: obj, alacakINT: alacakB, borcINT: borcB },
                dataType: "json",
                success: function (gelenDeg) {

                    if (gelenDeg.sonuc == "0") {
                        setTimeout(oTable.ajax.reload(), 300);
                        $('#UyeOl').modal('hide');
                        swal("Uyarı", gelenDeg.Message, "warning")
                            .then((value) => {
                                $('#UyeOl').modal('show');
                            });
                    }
                    else if (gelenDeg.sonuc == "3") {
                        setTimeout(oTable.ajax.reload(), 300);
                        $('#UyeOl').modal('hide');
                        swal("Hata", gelenDeg.Message, "error")
                            .then((value) => {
                                $('#UyeOl').modal('show');
                            });
                    }
                    else if (gelenDeg.sonuc == "1") {
                        swal("Başarılı", gelenDeg.Message, "success");
                        {
                            $('#UyeOl').modal('hide');
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
