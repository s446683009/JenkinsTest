
module.exports = {
    plugins: [
      [
        'import',
        {
          libraryName: '',
          camel2DashComponentName: false, // 是否需要驼峰转短线
          camel2UnderlineComponentName: false, // 是否需要驼峰转下划线
        },
      ],
    ],
    presets: ['module:metro-react-native-babel-preset'],
}