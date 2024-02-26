const path = require("path");

module.exports = {
  webpack: {
    alias: {
      "@common-components": path.resolve(__dirname, "src/components/Common"),
      "@components": path.resolve(__dirname, "src/components"),
      "@auth": path.resolve(__dirname, "src/core/auth"),
      "@routes": path.resolve(__dirname, "src/core/routes"),
      "@models": path.resolve(__dirname, "src/models"),
      "@services": path.resolve(__dirname, "src/services"),
      "@assets": path.resolve(__dirname, "src/shared/assets"),
      "@hooks": path.resolve(__dirname, "src/shared/hooks"),
      "@utils": path.resolve(__dirname, "src/shared/utils"),
      "@test-utils": path.resolve(__dirname, "src/shared/utils/test-utils"),
      "@context": path.resolve(__dirname, "src/state/context"),
      "@stores": path.resolve(__dirname, "src/state/stores"),
      "@": path.resolve(__dirname, "src"),
    },
  },
};
