const token = "@ViewBag.Token";
const socket = new WebSocket(`wss://ws.finnhub.io?token=${token}`);

// Connection opened -> Subscribe
socket.addEventListener("open", function (event) {
  socket.send(
    JSON.stringify({ type: "subscribe", symbol: "@Model.StockSymbol" })
  );
});

// Listen for messages
socket.addEventListener("message", function (event) {
  const stockData = JSON.parse(event.data)?.["data"]?.[0];
  if (stockData) {
    document.getElementById("stock-price").innerText = stockData.p;
  }
});

// Unsubscribe
var unsubscribe = function (symbol) {
  socket.send(JSON.stringify({ type: "unsubscribe", symbol: symbol }));
};
