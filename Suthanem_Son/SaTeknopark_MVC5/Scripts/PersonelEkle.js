function Yetkiler() {

    $('#stokeg').prop('checked', true);
            $('#StokSil').prop('checked', true);
            $('#CariEG').prop('checked', true);
            $('#CariSil').prop('checked', true);
            $('#FaturaEG').prop('checked', true);
            $('#FaturaSil').prop('checked', true);
            $('#KasaEG').prop('checked', true);
            $('#KasaSil').prop('checked', true);
            $('#BankaEG').prop('checked', true);
            $('#BankaSil').prop('checked', true);
            $('#ServisEG').prop('checked', true);
            $('#ServisSil').prop('checked', true);
            $('#PersonelEG').prop('checked', true);
            $('#PersonelSil').prop('checked', true);
            $('#MaasEG').prop('checked', true);
            $('#MaasSil').prop('checked', true);
            $('#KrediKSil').prop('checked', true);
            $('#KrediKEG').prop('checked', true);
        } function YetkiKaldir() {

            $('#stokeg').prop('checked', false);
            $('#StokSil').prop('checked', false);
            $('#CariEG').prop('checked', false);
            $('#CariSil').prop('checked', false);
            $('#FaturaEG').prop('checked', false);
            $('#FaturaSil').prop('checked', false);
            $('#KasaEG').prop('checked', false);
            $('#KasaSil').prop('checked', false);
            $('#BankaEG').prop('checked', false);
            $('#BankaSil').prop('checked', false);
            $('#ServisEG').prop('checked', false);
            $('#ServisSil').prop('checked', false);
            $('#PersonelEG').prop('checked', false);
            $('#PersonelSil').prop('checked', false);
            $('#MaasEG').prop('checked', false);
            $('#MaasSil').prop('checked', false);
            $('#KrediKSil').prop('checked', false);
            $('#KrediKEG').prop('checked', false);
        }

        $("#veri").on("submit", function (event) {
            event.preventDefault();
            var stokeg = false;
            var StokSil = false;
            var CariEG = false;
            var CariSil = false;
            var FaturaEG = false;
            var FaturaSil = false;
            var KasaEG = false;
            var KasaSil = false;
            var BankaEG = false;
            var BankaSil = false;
            var ServisEG = false;
            var ServisSil = false;
            var PersonelEG = false;
            var PersonelSil = false;
            var MaasEG = false;
            var MaasSil = false;
            var KrediKEG = false;
            var KrediKSil = false;

            if ($('#stokeg').prop('checked') == true) {
                stokeg = true;
            }
            if ($('#StokSil').prop('checked') == true) {
                StokSil = true;
            }
            if ($('#CariEG').prop('checked') == true) {
                CariEG = true;
            }
            if ($('#CariSil').prop('checked') == true) {
                CariSil = true;
            }
            if ($('#FaturaEG').prop('checked') == true) {
                FaturaEG = true;
            }
            if ($('#FaturaSil').prop('checked') == true) {
                FaturaSil = true;
            }
            if ($('#KasaEG').prop('checked') == true) {
                KasaEG = true;
            }
            if ($('#KasaSil').prop('checked') == true) {
                KasaSil = true;
            }
            if ($('#BankaEG').prop('checked') == true) {
                BankaEG = true;
            }
            if ($('#BankaSil').prop('checked') == true) {
                BankaSil = true;
            }
            if ($('#ServisEG').prop('checked') == true) {
                ServisEG = true;
            }
            if ($('#ServisSil').prop('checked') == true) {
                ServisSil = true;
            }
            if ($('#PersonelEG').prop('checked') == true) {
                PersonelEG = true;
            }
            if ($('#PersonelSil').prop('checked') == true) {
                PersonelSil = true;
            }
            if ($('#MaasEG').prop('checked') == true) {
                MaasEG = true;
            }
            if ($('#MaasSil').prop('checked') == true) {
                MaasSil = true;
            }
            if ($('#KrediKEG').prop('checked') == true) {
                KrediKEG = true;
            }
            if ($('#KrediKEG').prop('KrediKSil') == true) {
                KrediKSil = true;
            }





            var obj = {
                id: $("#pID").val(),
                Adi: $("#Adi").val(),
                Soyadi: $("#Soyadi").val(),
                PersonelGrubu: $("#Pozisyon").val(),
                Milleti: $("#Millet").val(),
                TCNo: $("#TcNo").val(),
                Cinsiyet: $("#Cinsiyet").val(),
                Telefon1: $("#Tel").val(),
                Adres: $("#Adres").val(),
                EkBilgiler: $("#HesKodu").val(),
                BabaAdi: $("#BabaAdi").val(),
                Maasi: $("#Maas").val(),
                IL: $("#Il").val(),
                Ilce: $("#Ilce").val(),
                EPosta: $("#Email").val(),
                SicilNo: $("#SicilNo").val(),
                WebKA: $("#WebKA").val(),
                WebSifre: $("#WebSifre").val(),
                Telefon2: $("#GSM").val(),
                vKasaID: $("#ThisKasaID").val(),
                vBankaID: $("#ThisBankaID").val(),
            };

            var yetki = {
                StokEG,
                StokSil,
                CariEG,
                CariSil,
                FaturaEG,
                FaturaSil,
                KasaEG,
                KasaSil,
                BankaEG,
                BankaSil,
                ServisEG,
                ServisSil,
                PersonelEG,
                PersonelSil,
                MaasEG,
                MaasSil,
                KrediKEG,
                KrediKSil,
            };




            $.ajax({
                type: "POST",
                url: '/Personel/yeniPersonel',
                data: { pr: obj, yt: yetki },
                dataType: "json",
                success: function (gelenDeg) {

                    if (gelenDeg.sonuc == "0") {
                        $('#PersonelEkle').modal('hide');
                        swal("Uyarı", gelenDeg.Message, "warning")
                            .then((value) => {
                                $('#PersonelEkle').modal('show');

                            });
                    }
                    else if (gelenDeg.sonuc == "3") {
                        $('#PersonelEkle').modal('hide');
                        swal("Hata", gelenDeg.Message, "error")
                            .then((value) => {
                                $('#PersonelEkle').modal('show');

                            });

                    }
                    else if (gelenDeg.sonuc == "1") {
                        swal("Başarılı", gelenDeg.Message, "success");
                        {
                            $('#PersonelEkle').modal('hide');

                            setTimeout(oTable.ajax.reload(), 300);
                        }
                    }
                },
                error: function () {
                    swal("Hata", "Kayıt Eklenirken Hata Oluştu!", "error");
                }
            });
});


function Goster(id) {

    $.ajax({
        type: "GET",
        url: '@Url.Action("PersonelBilgi", "Personel")/' + id,
        success: function (gt) {

            if (gt.success) {

                $("#pID").val(gt.data.pr.ID);
                $("#Adi").val(gt.data.pr.Adi);
                $("#Soyadi").val(gt.data.pr.Soyadi);
                $("#Pozisyon").val(gt.data.pr.PersonelGrubu);
                $("#Millet").val(gt.data.pr.Milleti);
                $("#TcNo").val(gt.data.pr.TCNo);
                $("#Cinsiyet").val(gt.data.pr.Cinsiyet);
                $("#Tel").val(gt.data.pr.Telefon1);
                $("#Adres").val(gt.data.pr.Adres);
                $("#HesKodu").val(gt.data.pr.EkBilgiler);
                $("#BabaAdi").val(gt.data.pr.BabaAdi);
                $("#Maas").val(gt.data.pr.Maasi);
                $("#Il").val(gt.data.pr.IL);
                $("#Ilce").val(gt.data.pr.Ilce);
                $("#Email").val(gt.data.pr.EPosta);
                $("#SicilNo").val(gt.data.pr.SicilNo);
                $("#WebKA").val(gt.data.pr.WebKA);
                $("#WebSifre").val(gt.data.pr.WebSifre);
                $("#GSM").val(gt.data.pr.Telefon2);
                $('#stokeg').prop('checked', gt.data.yt.stokeg);
                $('#StokSil').prop('checked', gt.data.yt.StokSil);
                $('#CariEG').prop('checked', gt.data.yt.CariEG);
                $('#CariSil').prop('checked', gt.data.yt.CariSil);
                $('#FaturaEG').prop('checked', gt.data.yt.FaturaEG);
                $('#FaturaSil').prop('checked', gt.data.yt.FaturaSil);
                $('#KasaEG').prop('checked', gt.data.yt.KasaEG);
                $('#KasaSil').prop('checked', gt.data.yt.KasaSil);
                $('#BankaEG').prop('checked', gt.data.yt.BankaEG);
                $('#BankaSil').prop('checked', gt.data.yt.BankaSil);
                $('#ServisEG').prop('checked', gt.data.yt.ServisEG);
                $('#ServisSil').prop('checked', gt.data.yt.ServisSil);
                $('#PersonelEG').prop('checked', gt.data.yt.PersonelEG);
                $('#PersonelSil').prop('checked', gt.data.yt.PersonelSil);
                $('#MaasEG').prop('checked', gt.data.yt.MaasEG);
                $('#MaasSil').prop('checked', gt.data.yt.MaasSil);
                $('#KrediKSil').prop('checked', gt.data.yt.KrediKSil);
                $('#KrediKEG').prop('checked', gt.data.yt.KrediKEG);
                $('#PersonelEkle').modal('show');

            }
        }

    });
}
