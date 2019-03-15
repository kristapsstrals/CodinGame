# Auto-generated code below aims at helping you parse
# the standard input according to the problem statement.

@n = gets.to_i # Number of elements which make up the association table.
@q = gets.to_i # Number Q of file names to be analyzed.

class HashClod < Hash
  def [](key)
    super _insensitive(key)
  end

  def []=(key, value)
    super _insensitive(key), value
  end

  # Keeping it DRY.
  protected

  def _insensitive(key)
    key.respond_to?(:upcase) ? key.upcase : key
  end
end

hash = HashClod.new
@n.times do
    # ext: file extension
    # mt: MIME type.
    ext, mt = gets.split(" ")
    
    hash[ext] = mt
end

@q.times do
    fname = gets.chomp # One file name per line.
    
    if fname.chars.last == '.'
        puts "UNKNOWN"
        next
    end
    
    arr = fname.split('.')
    if arr.length <= 1
        puts "UNKNOWN"
        next
    end
    
    test = arr.last
    
    test1 = hash[test]
    
    unless test1.nil?
        puts test1
    else
        puts "UNKNOWN"
    end
    
    STDERR.puts "FileName: #{fname}; file extension: #{test}; mime type: #{test1}"
end
