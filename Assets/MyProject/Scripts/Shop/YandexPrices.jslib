mergeInto(LibraryManager.library, {
  GetYandexCatalog: function () {
    if (!window.ysdk) {
      console.log("Yandex SDK not ready");
      return;
    }

    ysdk.getPayments().then(payments => {
      payments.getCatalog().then(products => {
        products.forEach(p => {
          const json = JSON.stringify({
            id: p.id,
            price: p.price,
            currency: p.currency
          });
          SendMessage(
            "YandexPriceReceiver",
            "OnReceivePrice",
            json
          );
        });
      });
    });
  }
});